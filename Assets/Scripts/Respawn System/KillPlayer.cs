using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public string deathName;
    public AudioClip SFX;
    [Range(0,1)]
    public float volume = 1f;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerController>().SetCanMove(false);
            collider.GetComponent<RespawnSystem>().KillPlayer(deathName, SFX, volume);
        }
    }
}
