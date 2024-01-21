using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private GameObject coinDrop;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int maxHealth;

    private int currentHealth;


    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        Instantiate(damageEffect, transform.position, Quaternion.identity);

        if (currentHealth <= 0) {
            Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
            Instantiate(coinDrop, transform.position, coinDrop.transform.rotation);
            Destroy(gameObject);
        }
    }
}
