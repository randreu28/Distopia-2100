using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform startPoint;
    public Transform endPoint;
    public float leverDuration = 0.33f;
    public float platformDuration = 6f;
    
    [Header("Plataforma")]
    public GameObject platform;

    [Header("SFX")]
    public AudioClip leverAudioClip;
    [Range(0, 1)]
    public float leverVolume = 1f;
    [Space(10)]
    public AudioClip platformAudioClip;
    [Range(0, 1)]
    public float platformVolume = 1f;

    private bool isClose = false;
    private bool isMoving = false;
    private bool hasMoved = false;
    private AudioSource leverSFX;
    private AudioSource platformSFX;

    void Awake(){
        leverSFX = platform.AddComponent(typeof(AudioSource)) as AudioSource;
        leverSFX.clip = leverAudioClip;
        leverSFX.volume = leverVolume;

        platformSFX = platform.AddComponent(typeof(AudioSource)) as AudioSource;
        platformSFX.clip = platformAudioClip;
        platformSFX.volume = platformVolume;
    }

    public void action(){
        if(!isMoving && isClose)
        {
            StartCoroutine(handleAnimations());
        }
    }

    private IEnumerator handleAnimations()
    {
        Vector3 targetRotation = hasMoved ? new Vector3(this.transform.eulerAngles.x + 45, this.transform.eulerAngles.y, this.transform.eulerAngles.z) : new Vector3(this.transform.eulerAngles.x - 45, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        yield return LerpRotation(this.gameObject, this.transform.eulerAngles, targetRotation, leverDuration);
        leverSFX.Play();
        platformSFX.Play();
        isMoving = true;
        if (hasMoved)
        {
            yield return LerpPosition(platform, endPoint.position, startPoint.position, platformDuration);
            hasMoved = !hasMoved;
        }
        else
        {
            yield return LerpPosition(platform, startPoint.position, endPoint.position, platformDuration);
            hasMoved = !hasMoved;
        }
        isMoving = false;
    }

    public IEnumerator LerpPosition(GameObject myObject, Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;
        myObject.transform.position = startPosition;
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            myObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        myObject.transform.position = targetPosition;
    }

    public IEnumerator LerpRotation(GameObject myObject, Vector3 startRotation, Vector3 targetRotation, float duration)
    {
        float time = 0;
        myObject.transform.eulerAngles = startRotation;
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            myObject.transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, t);
            time += Time.deltaTime;
            yield return null;
        }
        myObject.transform.eulerAngles = targetRotation;
    }

    private void OnTriggerEnter(Collider _is)
    {
        if(_is.gameObject.tag == "Player")
        {
            isClose = true;
        }
    }

    private void OnTriggerExit(Collider _is)
    {
        if(_is.gameObject.tag == "Player")
        {
            isClose = false;
        }
    }
}
