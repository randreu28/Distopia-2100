using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableFall : MonoBehaviour
{
    public AudioClip AudioClip;
    [Range(0, 1)]
    public float volume = 1f;
    public Transform resetPosition;

    private AudioSource SFX;

    void Awake(){
        SFX = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        SFX.clip = AudioClip;
        SFX.volume = volume;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pushable")
        {
            SFX.Play();
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Physics.SyncTransforms();
            other.gameObject.transform.position = resetPosition.position;
            other.gameObject.transform.rotation = resetPosition.rotation;
        }
    }
}
