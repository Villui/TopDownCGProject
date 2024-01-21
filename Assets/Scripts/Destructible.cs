using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    [SerializeField] private GameObject destructionEffect;
    [SerializeField] private GameObject coinEffect;


    public void Destroy() {
        Instantiate(destructionEffect, transform.position, destructionEffect.transform.rotation);
        Instantiate(coinEffect, transform.position, coinEffect.transform.rotation);
        Destroy(gameObject);
    }

}
