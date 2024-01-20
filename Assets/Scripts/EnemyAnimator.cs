using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private const string ENEMY_IS_MOVING = "IsMoving";
    private const string ENEMY_ATTACK_TRIGGER = "Attack";
    private const string ENEMY_MOVE_TRIGGER = "Move";
    private const string ENEMY_IDLE_TRIGGER = "Idle";

    private Enemy enemy;
    private EnemyAttack enemyAttack;
    private Animator animator;


    private void Awake() {
        enemy = GetComponent<Enemy>();
        enemyAttack = GetComponent<EnemyAttack>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        enemy.OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(EnemyState state) {
        //string animatorParameter = null;

        switch (state) {
            case EnemyState.Idle:
                animator.SetTrigger(ENEMY_IDLE_TRIGGER);
                break;

            case EnemyState.Follow:
                animator.SetTrigger(ENEMY_MOVE_TRIGGER);
                break;

            case EnemyState.Attack:
                if (enemyAttack.IsAttackOnCooldown()) {
                    animator.SetTrigger(ENEMY_IDLE_TRIGGER);
                    break;
                }

                animator.SetTrigger(ENEMY_ATTACK_TRIGGER);
                break;
        }

        //SetAnimatorParametersToFalse(animatorParameter);
    }

    private void SetAnimatorParametersToFalse(string exception = null) {
        foreach (AnimatorControllerParameter animatorParameter in animator.parameters) {
            if (animatorParameter.name == exception) continue;

            if (animatorParameter.type == AnimatorControllerParameterType.Bool) {
                animator.SetBool(animatorParameter.name, false);
            }
        }
    }
}
