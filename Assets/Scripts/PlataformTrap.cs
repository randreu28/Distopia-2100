using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformTrap : MonoBehaviour
    
{
    private bool isTrap = true;

    void OnTriggerEnter(Collider other){
        if(isTrap)
        {
            gameObject.AddComponent<Rigidbody>(); // Add the rigidbody.
            isTrap = false;
        }
    }
}
