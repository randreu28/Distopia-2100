using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform spawnPoint;

    void OnTriggerEnter(Collider collider)
    {

        if(collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            collider.gameObject.transform.position = spawnPoint.position;
            Physics.SyncTransforms();
        }
    }
}
