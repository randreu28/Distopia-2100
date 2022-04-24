using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    public AudioClip BackgroundMusic;
    [Range(0, 1)] public float MovingAudioVolume = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(BackgroundMusic, transform.position, MovingAudioVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
