using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cepelin : MonoBehaviour
{
    public Vector3 distance;
    public float cycleTime;
    public float waitTime = 1;


    private bool isMoving = false;
    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        yield return StartCoroutine(LerpPosition(_startPosition, _startPosition + distance, cycleTime));
        yield return StartCoroutine(wait(waitTime));
        yield return StartCoroutine(LerpPosition(_startPosition + distance, _startPosition, cycleTime));
        yield return StartCoroutine(wait(waitTime));
        yield return StartCoroutine(Loop());
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
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
