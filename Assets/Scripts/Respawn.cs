using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Player;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == Player)
        {
            var SpawnPointHolder = Player.GetComponent<SpawnPointHolder>();
            collider.gameObject.transform.position = SpawnPointHolder.SpawnPoint.position;
            Physics.SyncTransforms();
        }
    }
}
