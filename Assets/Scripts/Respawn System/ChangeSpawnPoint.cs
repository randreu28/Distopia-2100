using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnPoint : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnPoint;
    public GameObject Flag;
    [Range(0, 200)]
    public float LightIntensity = 75f;

    private bool isUsed = false;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == Player && !isUsed) 
        {
            var RespawnSystem = Player.GetComponent<RespawnSystem>();
            RespawnSystem.SpawnPoint = SpawnPoint;
            Flag.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.red * LightIntensity);
        }
    }
}
