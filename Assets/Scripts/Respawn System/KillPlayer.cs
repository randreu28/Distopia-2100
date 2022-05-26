using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public string DeathName;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerController>().SetCanMove(false);
            collider.GetComponent<RespawnSystem>().KillPlayer(DeathName);
        }
    }
}
