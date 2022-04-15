using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class SimpleControls : MonoBehaviour{
    public float playerVelocity = 10f;
    private Vector3 movement = new Vector3(0,0,0);

    Animator _Animator;

    void Start(){
        _Animator = GetComponent<Animator>();
    }

    void Update(){
        transform.position += movement * playerVelocity * Time.deltaTime;
    }

    private void OnWStart () { /* Habrá alguna manera de tener una sola funcion con una variable y dentro de ella detectar hold & release? */
        movement = Vector3.forward;
        transform.localRotation = Quaternion.Euler(0,0,0);
        _Animator.Play("running");  
        Debug.Log("W");
    }
    private void OnWEnd () {
        movement = Vector3.zero;
        _Animator.Play("Idle2");  
    }

    private void OnSStart () {
        movement = Vector3.back;
        transform.localRotation = Quaternion.Euler(0,180,0);
        _Animator.Play("running");  
        Debug.Log("S");
    }
    private void OnSEnd () {
        movement = Vector3.zero;
        _Animator.Play("Idle2");  
    }

    private void OnAStart () {
        movement = Vector3.left;
        transform.localRotation = Quaternion.Euler(0,-90,0);
        _Animator.Play("running");  
        Debug.Log("A");
    }
    private void OnAEnd () {
        movement = Vector3.zero;
        _Animator.Play("Idle2");  
    }

    private void OnDStart () {
        movement = Vector3.right;
        transform.localRotation = Quaternion.Euler(0,90,0);
        _Animator.Play("running");  
        Debug.Log("D");
    }
    private void OnDEnd () {
        movement = Vector3.zero;
        _Animator.Play("Idle2");  
    }

    private void OnSpaceStart () {
        movement = Vector3.up;
        _Animator.Play("jumping up");  
        Debug.Log("Space");
    }
    private void OnSpaceEnd () {
        movement = Vector3.zero;
        _Animator.Play("Idle2");  
    }
}
