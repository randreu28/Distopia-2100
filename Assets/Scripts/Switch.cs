using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch : MonoBehaviour
{

    Animator _switchAnimator;
    private bool isClose = false;
    public GameObject player;

    // Para tener de referencia para el puzle 6
    void Start()
    {
        _switchAnimator = GetComponent<Animator>();
    }
    void OnE(){
        if (isClose)
           _switchAnimator.SetBool("Switch", !_switchAnimator.GetBool("Switch"));

        Debug.Log("E");
    }
     private void OnTriggerEnter(Collider _is)
    {
        if(_is.gameObject == player)
         isClose = true;
    }

    private void OnTriggerExit(Collider _is)
    {
        if(_is.gameObject == player)
         isClose = false;
    }

}

