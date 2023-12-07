using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager Instance { get; private set; }

    [SerializeField] private LayerMask ignoreMouseLayerMask;

    private PlayerControls playerControls;
    private Vector3 mouseWorldPos;


    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;

        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    private void FixedUpdate() {
        SetMouseWorldPosition();
    }

    public Vector3 GetMovementVectorNormalized() {
        Vector2 inputVectorNormalized = playerControls.Player.Move.ReadValue<Vector2>();
        Vector3 movementVectorNormalized = new Vector3(inputVectorNormalized.x, 0f, inputVectorNormalized.y);

        return movementVectorNormalized;
    }

    private void SetMouseWorldPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 1000f, ~ignoreMouseLayerMask)) {
            Debug.LogError("Mouse raycast didn't hit anything");
            mouseWorldPos = Vector3.zero;
        }

        mouseWorldPos = hit.point;
    }

    public Vector3 GetMouseWorldPosition() {
        return mouseWorldPos;
    }
}
