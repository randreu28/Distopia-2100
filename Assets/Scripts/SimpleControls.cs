using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class SimpleControls : MonoBehaviour{
    public float playerVelocity = 10f;
    private Vector3 movement = new Vector3(0,0,0);

    void Update(){
        transform.position += movement * playerVelocity * Time.deltaTime;
    }

    private void OnWStart () { /* Habrá alguna manera de tener una sola funcion con una variable y dentro de ella detectar hold & release? */
        movement = Vector3.forward;
        Debug.Log("W");
    }
    private void OnWEnd () {
        movement = Vector3.zero;
    }

    private void OnSStart () {
        movement = Vector3.back;
        Debug.Log("S");
    }
    private void OnSEnd () {
        movement = Vector3.zero;
    }

    private void OnAStart () {
        movement = Vector3.left;
        Debug.Log("A");
    }
    private void OnAEnd () {
        movement = Vector3.zero;
    }

    private void OnDStart () {
        movement = Vector3.right;
        Debug.Log("D");
    }
    private void OnDEnd () {
        movement = Vector3.zero;
    }
}
