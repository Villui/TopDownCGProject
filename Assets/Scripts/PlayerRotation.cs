using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    private Vector3 lookDirection;


    private void Update() {
        Rotate();
    }

    private void Rotate() {
        Vector3 mouseWorldPos = InputManager.Instance.GetMouseWorldPosition();
        lookDirection =  mouseWorldPos - transform.position;

        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        rotation.x = 0f;
        rotation.z = 0f;

        transform.rotation = rotation;
    }
}
