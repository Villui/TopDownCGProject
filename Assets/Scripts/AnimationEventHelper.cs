using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour {

    public event Action OnReadyToAttack;

    [SerializeField] private Transform effectSpawnPoint;
    [SerializeField] private GameObject effect;


    public void ReadyToAttack() {
        OnReadyToAttack?.Invoke();
    }

    public void PlayEffect() {
        Instantiate(effect, effectSpawnPoint.position, effect.transform.rotation);
    }
}
