using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {

    [SerializeField] private GameObject[] ammos;
    [SerializeField] private Image reloadImage; 


    private void Start() {
        PlayerAttack.Instance.OnShoot += HandleShoot;
        PlayerAttack.Instance.OnReload += HandleReload;
    }

    private void HandleReload() {
        StartCoroutine(ReloadRoutine());
    }

    private void Reloaded() {
        foreach (GameObject ammo in ammos) {
            ammo.SetActive(true);
        }
    }

    private void HandleShoot() {
        int ammoLeft = PlayerAttack.Instance.GetCurrentAmmoLeft();

        if (ammoLeft == 0) return;

        ammos[ammoLeft - 1].SetActive(false);
    }

    private IEnumerator ReloadRoutine() {
        float fillAmount = 1 / PlayerAttack.Instance.GetReloadTime() * Time.deltaTime;
        float fillTime = 0f;

        while (fillTime < PlayerAttack.Instance.GetReloadTime()) {
            reloadImage.fillAmount += fillAmount;
            fillTime += Time.deltaTime;

            yield return null;
        }

        Reloaded();
        reloadImage.fillAmount = 0;
    }
}
