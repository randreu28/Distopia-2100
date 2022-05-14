using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnPoint : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnPoint;
    public GameObject Flag;

    private bool isUsed = false;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == Player && !isUsed) 
        {
            var SpawnPointHolder = Player.GetComponent<SpawnPointHolder>();
            SpawnPointHolder.SpawnPoint = SpawnPoint;
            Flag.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
        }
    }
}
