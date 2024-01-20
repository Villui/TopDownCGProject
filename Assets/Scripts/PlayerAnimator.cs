using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string PLAYER_STOP_TRIGGER = "Stop";
    private const string PLAYER_SHOOT_TRIGGER = "Shoot";
    private const string PLAYER_SLASH_TRIGGER = "Slash";
    private const string PLAYER_WALK_TRIGGER = "Walk";
    private const string PLAYER_WALK_LEFT_TRIGGER = "WalkLeft";
    private const string PLAYER_WALK_RIGHT_TRIGGER = "WalkRight";
    private const string PLAYER_WALK_BACKWARDS_TRIGGER = "WalkBackwards";

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
        playerAttack.OnSlash += HandlePlayerSlash;
    }

    private void HandlePlayerSlash() {
        animator.SetTrigger(PLAYER_SLASH_TRIGGER);
    }

    private void HandlePlayerShoot() {
        animator.SetTrigger(PLAYER_SHOOT_TRIGGER);
    }

    private void HandlePlayerStoppedMoving() {
        ResetAnimatorTriggers();
        animator.SetTrigger(PLAYER_STOP_TRIGGER);
    }

    private void HandlePlayerMoving(PlayerMovement.MovementType obj) {
        switch (obj) {
            case PlayerMovement.MovementType.Forward:
                animator.SetTrigger(PLAYER_WALK_TRIGGER);
                break;

            case PlayerMovement.MovementType.Backward:
                animator.SetTrigger(PLAYER_WALK_BACKWARDS_TRIGGER);
                break;

            case PlayerMovement.MovementType.LeftStrafe:
                animator.SetTrigger(PLAYER_WALK_LEFT_TRIGGER);
                break;

            case PlayerMovement.MovementType.RightStrafe:
                animator.SetTrigger(PLAYER_WALK_RIGHT_TRIGGER);
                break;
        }
    }

    private void ResetAnimatorTriggers(string exception = null) {
        foreach (AnimatorControllerParameter animatorParameter in animator.parameters) {
            if (animatorParameter.type == AnimatorControllerParameterType.Trigger) {
                animator.ResetTrigger(animatorParameter.name);
            }
        }
    }
}
