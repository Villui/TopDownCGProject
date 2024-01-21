using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

    [SerializeField] private float time = 3f;


    private void Start() {
        Destroy(gameObject, time);
    }
}
