using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public enum SlashStyle {
        None,
        Vertical,
        Horizontal
    }

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
    [SerializeField] private GameObject slashVFX1;
    [SerializeField] private GameObject slashVFX2;
    [SerializeField] private Transform slashSpawnOffset1;
    [SerializeField] private Transform slashSpawnOffset2;

    [SerializeField] private float shootCooldown;

    private float slashStyleChangeTime = 1f;
    private int currentAmmoLeft;
    private bool shootOnCooldown;
    private AnimationEventHelper animationEvents;
    private LayerMask enemyLayer;
    private SlashStyle currentSlashStyle;
    private Coroutine slashStyleRoutine;


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
        currentSlashStyle = SlashStyle.None;
    }

    private void HandleSlashActionPerformed() {
        OnSlash?.Invoke();
    }

    private void HandleReadyToSlash() {
        if (slashStyleRoutine != null) {
            StopCoroutine(slashStyleRoutine);
            slashStyleRoutine = null;
        }

        if (currentSlashStyle == SlashStyle.None) {
            GameObject newSlash = Instantiate(slashVFX1, slashSpawnOffset1);
            ParticleSystem ps = newSlash.GetComponentInChildren<ParticleSystem>();

            var main = ps.main;
            main.startRotationYMultiplier = (transform.rotation.eulerAngles.y - 47.9f) / 57.3f;
            ps.Play();
        }
        else if (currentSlashStyle == SlashStyle.Vertical) {
            GameObject newSlash = Instantiate(slashVFX2, slashSpawnOffset2);
            ParticleSystem ps = newSlash.GetComponentInChildren<ParticleSystem>();

            var main = ps.main;
            main.startRotationYMultiplier = (transform.rotation.eulerAngles.y - 130.4f) / 57.3f;
            ps.Play();
        }

        slashStyleRoutine = StartCoroutine(SlashStyleRoutine());
        StartCoroutine(SlashCheckRoutine());
    }

    private void HandleShootActionPerformed() {
        if (shootOnCooldown || currentAmmoLeft < 1) return;

        OnShoot?.Invoke();

        StartCoroutine(ShootRoutine());

        StartCoroutine(ShootCooldownRoutine());
        currentAmmoLeft -= 1;

        if (currentAmmoLeft < 1) {
            StartCoroutine(ReloadRoutine());
        }
    }

    // Need to delay so bullet doesn't go into the ground
    private IEnumerator ShootRoutine() {
        yield return new WaitForSeconds(0.05f);

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

    private IEnumerator SlashStyleRoutine() {
        if (currentSlashStyle == SlashStyle.None) {
            currentSlashStyle = SlashStyle.Vertical;
        }
        else if (currentSlashStyle == SlashStyle.Vertical) {
            currentSlashStyle = SlashStyle.Horizontal;
        }
        else if (currentSlashStyle == SlashStyle.Horizontal) {
            currentSlashStyle = SlashStyle.Vertical;
        }

        yield return new WaitForSeconds(slashStyleChangeTime);

        currentSlashStyle = SlashStyle.None;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(slashDamage);
        }
        if (other.gameObject.TryGetComponent<Destructible>(out Destructible destructible)) {
            destructible.Destroy();
        }
    }
}
