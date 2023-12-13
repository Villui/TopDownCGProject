using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string PLAYER_STOP_TRIGGER = "Stop";
    private const string PLAYER_IS_WALKING = "IsWalking";
    private const string PLAYER_IS_WALKING_BACKWARDS = "IsWalkingBackwards";
    private const string PLAYER_IS_WALKING_LEFT = "IsWalkingLeft";
    private const string PLAYER_IS_WALKING_RIGHT = "IsWalkingRight";
    private const string PLAYER_SHOOT_TRIGGER = "Shoot";

    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;


    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start() {
        playerMovement.OnPlayerMoving += HandlePlayerMoving;
        playerMovement.OnPlayerStoppedMoving += HandlePlayerStoppedMoving;
        playerAttack.OnShoot += HandlePlayerShoot;
    }

    private void HandlePlayerShoot() {
        animator.SetTrigger(PLAYER_SHOOT_TRIGGER);
        SetAnimatorParametersToFalse();
    }

    private void HandlePlayerStoppedMoving() {
        animator.SetTrigger(PLAYER_STOP_TRIGGER);
        SetAnimatorParametersToFalse();
    }

    private void HandlePlayerMoving(PlayerMovement.MovementType obj) {
        switch (obj) {
            case PlayerMovement.MovementType.Forward:
                SetAnimatorParametersToFalse(PLAYER_IS_WALKING);
                animator.SetBool(PLAYER_IS_WALKING, true);
                break;

            case PlayerMovement.MovementType.Backward:
                SetAnimatorParametersToFalse(PLAYER_IS_WALKING_BACKWARDS);
                animator.SetBool(PLAYER_IS_WALKING_BACKWARDS, true);
                break;

            case PlayerMovement.MovementType.LeftStrafe:
                SetAnimatorParametersToFalse(PLAYER_IS_WALKING_LEFT);
                animator.SetBool(PLAYER_IS_WALKING_LEFT, true);
                break;

            case PlayerMovement.MovementType.RightStrafe:
                SetAnimatorParametersToFalse(PLAYER_IS_WALKING_RIGHT);
                animator.SetBool(PLAYER_IS_WALKING_RIGHT, true);
                break;
        }
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
