using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpawnPoint : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject Flag;
    [Range(0, 200)]
    public float LightIntensity = 75f;

    public AudioClip AudioClip;
    [Range(0, 1)]
    public float volume = 1f;

    private AudioSource SFX;
    private bool isUsed = false;

    void Awake()
    {
        SFX = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        SFX.clip = AudioClip;
        SFX.volume = volume;
    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && !isUsed) 
        {
            RespawnSystem RS = collider.GetComponent<RespawnSystem>();
            
            RS.ChangeSpawn(SpawnPoint);
            RS.HandleFlag(Flag, LightIntensity);
            SFX.Play();
            isUsed = true;
        }
    }
}
