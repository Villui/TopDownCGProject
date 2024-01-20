using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private const EnemyState thisState = EnemyState.Attack;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldown;

    private bool thisStateEnabled;
    private AnimationEventHelper animationEvents;
    private Enemy enemy;

    private float attackCooldownTimer;
    private bool readyToAttack;


    private void Awake() {
        animationEvents = GetComponentInChildren<AnimationEventHelper>();
        enemy = GetComponent<Enemy>();
    }

    private void Start() {
        animationEvents.OnReadyToAttack += HandleReadyToAttack;
        enemy.OnStateChanged += HandleStateChanged;

        attackCooldownTimer = attackCooldown;
    }

    private void HandleReadyToAttack() {
        readyToAttack = true;
    }

    private void HandleStateChanged(EnemyState state) {
        if (state != thisState) {
            DisableState();
        }
        else {
            EnableState();
        }
    }

    private void Update() {
        if (!thisStateEnabled) return;

        attackCooldownTimer += Time.deltaTime;

        if (!readyToAttack) return;

        if (attackCooldownTimer >= attackCooldown) {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            attackCooldownTimer = 0f;
        }

        readyToAttack = false;
    }

    private void DisableState() {
        thisStateEnabled = false;
    }

    private void EnableState() {
        thisStateEnabled = true;
        transform.LookAt(enemy.GetTarget(), Vector3.up);
    }

    public bool IsAttackOnCooldown() {
        return attackCooldownTimer < attackCooldown;
    }
}
