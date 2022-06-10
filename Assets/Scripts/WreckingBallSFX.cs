using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBallSFX : MonoBehaviour
{
    private bool isDone = false; 
    private AudioSource SFX;
    
    void Start()
    {
        SFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wrecking Ball" && !isDone)
        {
            SFX.Play();
            isDone = true;
        }
    }
}
