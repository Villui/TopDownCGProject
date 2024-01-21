using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStand : MonoBehaviour {

    [SerializeField] private GameObject tooltipCanvas;

    private int swordCost = 50;
    private bool playerInCollider;


    private void Start() {
        InputManager.Instance.OnInteractPerformed += HandleInteractPerformed;
    }

    private void HandleInteractPerformed() {
        if (!playerInCollider) return;

        if (PlayerCoinManager.Instance.GetCurrentCoinAmount() < swordCost) return;

        Player.Instance.EquipEffectSword();
        PlayerCoinManager.Instance.RemoveCoins(swordCost);
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
