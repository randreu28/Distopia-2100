using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Parenter : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider _is)
    {
        if (_is.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider _is)
    {
        if (_is.gameObject == player)
        {
            player.transform.parent = null;
        }
    }

}