using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager Instance { get; private set; }

    public event Action OnShootPerformed;
    public event Action OnSlashPerformed;
    public event Action OnTargetSelectPerformed;
    public event Action OnInteractPerformed;

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

        playerControls.Player.Shoot.performed += HandleShootActionPerformed;
        playerControls.Player.Slash.performed += HandleSlashActionPerformed;
        playerControls.Player.TargetSelect.performed += HandleTargetSelectActionPerformed;
        playerControls.Player.Interact.performed += HandleInteractActionPerformed;
    }

    private void HandleInteractActionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractPerformed?.Invoke();
    }

    private void HandleTargetSelectActionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnTargetSelectPerformed?.Invoke();
    }

    private void HandleSlashActionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnSlashPerformed?.Invoke();
    }

    private void HandleShootActionPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnShootPerformed?.Invoke();
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
