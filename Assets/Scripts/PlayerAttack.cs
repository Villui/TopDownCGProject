using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public event Action OnShoot;


    private void Start() {
        InputManager.Instance.OnShootPerformed += HandleShootActionPerformed;
    }

    private void HandleShootActionPerformed() {
        OnShoot?.Invoke();
    }
}
