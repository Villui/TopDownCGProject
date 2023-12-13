using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private const string ENEMY_IS_MOVING = "IsMoving";

    private Enemy enemy;
    private Animator animator;


    private void Awake() {
        enemy = GetComponent<Enemy>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        enemy.OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(EnemyState state) {
        string animatorParameter = null;

        switch (state) {
            case EnemyState.Follow:
                animatorParameter = ENEMY_IS_MOVING;
                break;
        }

        if (animatorParameter != null) {
            animator.SetBool(animatorParameter, true);
        }

        SetAnimatorParametersToFalse(animatorParameter);
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
