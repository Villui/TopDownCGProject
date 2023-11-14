using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {


    private void Update() {
        Rotate();
    }

    private void Rotate() {
        Vector3 mouseWorldPos = InputManager.Instance.GetMouseWorldPosition();
        Vector3 lookDirection =  mouseWorldPos - transform.position;
        
        Quaternion rotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = rotation;
    }
}
