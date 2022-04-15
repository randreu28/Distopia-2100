using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch : MonoBehaviour
{

    Animator _switchAnimator;
    private bool isClose = false;

    // Para tener de referencia para el puzle 6
    void Start()
    {
        _switchAnimator = GetComponent<Animator>();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && isClose == true)
           _switchAnimator.SetBool("LightSwitch", !_switchAnimator.GetBool("LightSwitch"));
    }
     private void OnTriggerEnter(Collider other)
    {
         isClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
         isClose = false;
    }

}

