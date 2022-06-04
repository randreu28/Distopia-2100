using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            other.GetComponent<PlayerController>().SetCanMove(false);
        }
    }
}
