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
            var PlayerInfo = Player.GetComponent<PlayerInfo>();
            PlayerInfo.SpawnPoint = SpawnPoint;
            Flag.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.red * 75f);
        }
    }
}
