using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepelin : MonoBehaviour
{
    [Header ("Puntos a recorrer")]
    public Transform[] points;
    [Header ("Tiempo de interpolación de la posición entre punto y punto")]
    public float timePosition;
    [Header ("Tiempo de interpolación de la rotación entre punto y punto")]
    public float timeRotation;
    [Header ("Tiempo de espera entre punto y punto")]
    public float timeWait = 0f;

    private Transform currentTransform;

    void Start()
    {
        currentTransform = this.gameObject.transform;
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        for(int i = 0; i <= points.Length; i++)
        {
            yield return currentTransform = this.gameObject.transform;
            yield return StartCoroutine(LerpPosition(currentTransform.position, points[i].position, timePosition));
            yield return StartCoroutine(LerpRotation(currentTransform.rotation, points[i].rotation, timeRotation));
            yield return StartCoroutine(wait(timeWait));
        }
        //yield return StartCoroutine(Loop());
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator LerpPosition(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            //t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    IEnumerator LerpRotation(Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            //t = t * t * (3f - 2f * t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotation;
    }
}
