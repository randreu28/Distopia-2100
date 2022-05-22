using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float distanceUp = 5f;
    public float duration = 0.3f;
    public AudioClip AudioClip;
    [Range(0, 1)]
    public float volume = 1f;

    private bool isUp = false;
    private bool isMoving = false;
    private AudioSource SFX;

    void Awake(){
        SFX = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        SFX.clip = AudioClip;
        SFX.volume = volume;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            var endPosition = gameObject.transform.position + new Vector3(0, distanceUp, 0);
            if(!isUp && !isMoving)
            {
                SFX.Play();
                StartCoroutine(LerpPosition(endPosition, duration));
                isUp = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            var endPosition = gameObject.transform.position - new Vector3(0, distanceUp, 0);
            if(isUp && !isMoving)
            {
                SFX.Play();
                StartCoroutine(LerpPosition(endPosition, duration));
                isUp = false;
            }
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        isMoving = true;
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }
}
