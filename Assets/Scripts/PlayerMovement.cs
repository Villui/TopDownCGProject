using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public enum MovementType {
        Forward,
        Backward,
        RightStrafe,
        LeftStrafe
    }

    public event Action<MovementType> OnPlayerMoving;
    public event Action OnPlayerStoppedMoving;

    [SerializeField] private float playerMoveSpeed;

    private Rigidbody playerRb;
    private Vector3 movementVectorNormalized;
    private MovementType currentMovementType;

    private bool playerIsStopped;


    private void Awake() {
        playerRb = GetComponent<Rigidbody>();

        playerIsStopped = true;
    }

    private void Update() {
        GetMovementVectorNormalized();
    }

    private void FixedUpdate() {
        if (movementVectorNormalized.sqrMagnitude > 0f) {
            if (playerIsStopped || GetMovementType()) {
                OnPlayerMoving?.Invoke(currentMovementType);
            }

            Move();
            playerIsStopped = false;
        }
        else if (!playerIsStopped) {
            OnPlayerStoppedMoving?.Invoke();
            playerIsStopped = true;
        }
    }

    private void Move() {
        Vector3 velocity = playerRb.velocity;

        velocity.x = movementVectorNormalized.x * playerMoveSpeed * Time.deltaTime;
        velocity.z = movementVectorNormalized.z * playerMoveSpeed * Time.deltaTime;

        playerRb.velocity = velocity;
    }

    private void GetMovementVectorNormalized() {
        movementVectorNormalized = InputManager.Instance.GetMovementVectorNormalized();
    }

    private bool GetMovementType() {
        MovementType previousMovementType = currentMovementType;

        if (Vector3.Dot(transform.forward, movementVectorNormalized) < -0.3f) {
            currentMovementType = MovementType.Backward;
        }
        else if (Vector3.Dot(transform.forward, movementVectorNormalized) > 0.3f) {
            currentMovementType = MovementType.Forward;
        }
        else if (Vector3.Dot(transform.forward, movementVectorNormalized) >= -0.3f && 
            Vector3.Dot(transform.forward, movementVectorNormalized) <= 0) {
            currentMovementType = MovementType.RightStrafe;
        }
        else {
            currentMovementType = MovementType.LeftStrafe;
        }

        return previousMovementType == currentMovementType;
    }
}
