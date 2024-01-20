using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public static PlayerAttack Instance;

    public event Action OnShoot;
    public event Action OnSlash;
    public event Action OnReload;

    [SerializeField] private float reloadTime;
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private int slashDamage = 4;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private GameObject swordHitbox;
    [SerializeField] private float slashHitboxRadius = 1f;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float shootCooldown;

    private int currentAmmoLeft;
    private bool shootOnCooldown;
    private AnimationEventHelper animationEvents;
    LayerMask enemyLayer;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player Attack Instance already exists!");
            return;
        }

        Instance = this;

        animationEvents = GetComponentInChildren<AnimationEventHelper>();
    }

    private void Start() {
        InputManager.Instance.OnShootPerformed += HandleShootActionPerformed;
        InputManager.Instance.OnSlashPerformed += HandleSlashActionPerformed;

        animationEvents.OnReadyToAttack += HandleReadyToSlash;
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        currentAmmoLeft = maxAmmo;
    }

    private void HandleSlashActionPerformed() {
        OnSlash?.Invoke();
    }

    private void HandleReadyToSlash() {
        StartCoroutine(SlashCheckRoutine());
    }

    private void HandleShootActionPerformed() {
        if (shootOnCooldown || currentAmmoLeft < 1) return;

        OnShoot?.Invoke();

        GameObject target = PlayerTargetSelection.Instance.GetCurrentTarget();

        if (target == null) {
            GameObject newBullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);
        }
        else {
            GameObject newBullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);

            Projectile projectile = newBullet.GetComponent<Projectile>();
            Vector3 directionToEnemy = target.transform.position - shootOrigin.transform.position;

            projectile.SetDirection(directionToEnemy);
        }

        StartCoroutine(ShootCooldownRoutine());
        currentAmmoLeft -= 1;

        if (currentAmmoLeft < 1) {
            StartCoroutine(ReloadRoutine());
        }
    }

    public int GetCurrentAmmoLeft() {
        return currentAmmoLeft;
    }

    public int GetMaxAmmo() {
        return maxAmmo;
    }

    public float GetReloadTime() {
        return reloadTime;
    }

    private IEnumerator ShootCooldownRoutine() {
        shootOnCooldown = true;

        yield return new WaitForSeconds(shootCooldown);

        shootOnCooldown = false;
    }

    private IEnumerator ReloadRoutine() {
        OnReload?.Invoke();

        yield return new WaitForSeconds(reloadTime);

        currentAmmoLeft = maxAmmo;
    }

    private IEnumerator SlashCheckRoutine() {
        swordHitbox.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        swordHitbox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(slashDamage);
        }
    }
}
