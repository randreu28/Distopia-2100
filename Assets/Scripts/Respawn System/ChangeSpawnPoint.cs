using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnPoint : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject Flag;
    [Range(0, 200)]
    public float LightIntensity = 75f;

    private bool isUsed = false;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && !isUsed) 
        {
            RespawnSystem RS = collider.GetComponent<RespawnSystem>();
            
            RS.ChangeSpawn(SpawnPoint);
            RS.HandleFlag(Flag, LightIntensity);
        }
    }
}
