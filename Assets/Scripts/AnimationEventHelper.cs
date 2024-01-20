using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour {

    public event Action OnReadyToAttack;


    public void ReadyToAttack() {
        OnReadyToAttack?.Invoke();
    }
}
