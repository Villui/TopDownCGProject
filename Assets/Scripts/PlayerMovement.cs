using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float playerMoveSpeed;

    private Rigidbody playerRb;
    private Vector3 movementVectorNormalized;


    private void Awake() {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update() {
        GetMovementVectorNormalized();
    }

    private void FixedUpdate() {
        if (movementVectorNormalized.sqrMagnitude > 0f) {
            Move();
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
}
