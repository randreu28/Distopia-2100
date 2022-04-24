using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Switch : MonoBehaviour
{

    private Animator _switchAnimator;
    private Animator _playerAnimator;
    private Animator _platformAnimator;
    private bool isClose = false;
    public GameObject player;
    public GameObject platform;

    // Para tener de referencia para el puzle 6
    void Start()
    {
        _switchAnimator = GetComponentInChildren<Animator>();
        _playerAnimator = player.GetComponent<Animator>();
        _platformAnimator = platform.GetComponent<Animator>();
    }
    public void action(){
        if (isClose){
           _switchAnimator.SetBool("Switch", !_switchAnimator.GetBool("Switch"));
            _platformAnimator.SetBool("isOn", !_platformAnimator.GetBool("isOn"));
            _playerAnimator.SetBool("PressButton", true);
           StartCoroutine(ActionAnimation());
        }
    }
    IEnumerator ActionAnimation(){
        yield return new WaitForSeconds(1);
        _playerAnimator.SetBool("PressButton", false);
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

