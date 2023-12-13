using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    public event Action<EnemyState> OnStateChanged;

    [SerializeField] float followRange;
    [SerializeField] float attackRange;

    private Transform target;
    private EnemyState enemyState;


    private void Start() {
        target = Player.Instance.transform;
    }

    private void Update() {
        CheckDistanceToPlayer();
    }

    private void CheckDistanceToPlayer() {
        if (Vector3.Distance(transform.position, target.position) <= attackRange) {
            SwapState(EnemyState.Attack);
        }
        else if (Vector3.Distance(transform.position, target.position) <= followRange) {
            SwapState(EnemyState.Follow);
        }
        else {
            SwapState(EnemyState.Idle);
        }
    }

    private void SwapState(EnemyState state) {
        switch (state) {
            case EnemyState.Idle:
                enemyState = EnemyState.Idle;
                OnStateChanged?.Invoke(enemyState); 
                break;

            case EnemyState.Follow:
                enemyState = EnemyState.Follow;
                OnStateChanged?.Invoke(enemyState);
                break;
                
            case EnemyState.Attack:
                enemyState = EnemyState.Attack;
                OnStateChanged?.Invoke(enemyState);
                break;
        }
    }

    public Transform GetTarget() {
        return target;
    }
}

public enum EnemyState {
    Idle,
    Follow,
    Attack
}
