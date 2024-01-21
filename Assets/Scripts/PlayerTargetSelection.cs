using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetSelection : MonoBehaviour {

    public static PlayerTargetSelection Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private float scanRadius = 10f;

    private Vector3 originalCamRotation;
    private GameObject currentTarget;
    private bool targetFocused;
    LayerMask enemyLayer;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player Target Selection Instance already exists!");
            return;
        }

        Instance = this;
    }

    private void Start() {
        InputManager.Instance.OnTargetSelectPerformed += ScanTargets;
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        originalCamRotation = virtualCam.transform.eulerAngles;
    }

    private void Update() {
        if (currentTarget == null && targetFocused) {
            ResetCamera();
        }

        if (currentTarget != null) {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > scanRadius) {
                currentTarget = null;
                ResetCamera();
            }
        }
    }

    private void ScanTargets() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, scanRadius, 0f, enemyLayer, QueryTriggerInteraction.UseGlobal);

        if (currentTarget != null && hits.Length == 0) {
            currentTarget = null;
            ResetCamera();
            return;
        }

        if (currentTarget != null && hits.Length == 1) {
            if (hits[0].collider.gameObject == currentTarget) {
                currentTarget = null;
                ResetCamera();
                return;
            }
        }

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == currentTarget) continue;

            currentTarget = hit.collider.gameObject;
            virtualCam.LookAt = currentTarget.transform;
            targetFocused = true;
            break;
        }
    }

    private void ResetCamera() {
        virtualCam.LookAt = null;
        virtualCam.transform.eulerAngles = originalCamRotation;
        targetFocused = false;
    }

    public GameObject GetCurrentTarget() {
        return currentTarget;
    }
}
