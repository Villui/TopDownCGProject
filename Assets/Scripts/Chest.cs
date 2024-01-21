using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private const string CHEST_TRIGGER = "Open";

    [SerializeField] private GameObject tooltipCanvas;

    private bool playerInCollider;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        InputManager.Instance.OnInteractPerformed += HandleInteractPerformed;
    }

    private void HandleInteractPerformed() {
        if (!playerInCollider) return;

        animator.SetTrigger(CHEST_TRIGGER);
        tooltipCanvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() == null) return;

        tooltipCanvas.SetActive(true);

        playerInCollider = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Player>() == null) return;

        tooltipCanvas.SetActive(false);

        playerInCollider = false;
    }
}
