using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour {

    [SerializeField] private int projectileDamage = 3;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float homingTime;

    private bool isHoming = true;


    private void Awake() {
        Destroy(gameObject, projectileLifetime);
        StartCoroutine(HomingRoutine());
    }

    private void Update() {
        if (isHoming) {
            transform.LookAt(Player.Instance.GetCenterOfMass());
        }

        Vector3 moveDirection = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        transform.position += moveDirection * projectileSpeed * Time.deltaTime;
    }

    private IEnumerator HomingRoutine() {
        yield return new WaitForSeconds(homingTime);

        isHoming = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth)) {
            playerHealth.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
