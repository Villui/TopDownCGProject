using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private int projectileDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;

    private Vector3 moveDirection;

    private void Awake() {
        Destroy(gameObject, projectileLifetime);
        moveDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);
    }

    private void Start() {
        transform.forward = moveDirection;
    }

    public void SetDirection(Vector3 direction) {
        moveDirection = direction;
        transform.forward = moveDirection;
    }

    private void Update() {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(projectileDamage);
            //play effect

            Destroy(gameObject);
        }
    }
}
