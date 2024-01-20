using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private GameObject damageEffect;
    [SerializeField] private int maxHealth;

    private int currentHealth;


    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
