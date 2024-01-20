using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private const EnemyState thisState = EnemyState.Follow;

    private bool thisStateEnabled = false;
    private Enemy enemy;
    private NavMeshAgent agent;


    private void Awake() {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        enemy.OnStateChanged += HandleStateChanged;
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

        agent.destination = enemy.GetTarget().position;
    }

    private void DisableState() {
        agent.destination = Vector3.zero;
        thisStateEnabled = false;
    }

    private void EnableState() {
        thisStateEnabled = true;
    }
}
