using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    [SerializeField] private GameObject destructionEffect;


    public void Destroy() {
        Instantiate(destructionEffect, transform.position, destructionEffect.transform.rotation);
        Destroy(gameObject);
    }

}
