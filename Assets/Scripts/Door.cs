using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private const string DOOR_TRIGGER = "Open";

    [SerializeField] private GameObject[] doors;

    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Open() {
        animator.SetTrigger(DOOR_TRIGGER);
    }
}
