using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    [SerializeField] private GameObject swordEffect;
    [SerializeField] private Transform centerOfMass;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player Instance already exists!");
            return;
        }

        Instance = this;
    }

    public Transform GetCenterOfMass() {
        return centerOfMass; 
    }

    public void EquipEffectSword() {
        swordEffect.SetActive(true);
    }
}
