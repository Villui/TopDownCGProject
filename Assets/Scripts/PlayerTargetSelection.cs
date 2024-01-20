using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetSelection : MonoBehaviour {

    public static PlayerTargetSelection Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private float scanRadius = 10f;

    private GameObject currentTarget;
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
    }

    private void Update() {
        if (currentTarget == null && virtualCam.LookAt != null) {
            virtualCam.LookAt = null;
        } 
    }

    private void ScanTargets() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, scanRadius, 0f, enemyLayer, QueryTriggerInteraction.UseGlobal);

        if (currentTarget != null && hits.Length == 0) {
            currentTarget = null;
            virtualCam.LookAt = null;
            return;
        } 

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == currentTarget) continue;

            currentTarget = hit.collider.gameObject;
            virtualCam.LookAt = currentTarget.transform;
            break;
        }
    }

    public GameObject GetCurrentTarget() {
        return currentTarget;
    }
}
