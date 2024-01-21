using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    private const string LEVER_TRIGGER = "Pull";

    [SerializeField] private GameObject tooltipCanvas;
    [SerializeField] private Door door;

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

        animator.SetTrigger(LEVER_TRIGGER);
        door.Open();
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
