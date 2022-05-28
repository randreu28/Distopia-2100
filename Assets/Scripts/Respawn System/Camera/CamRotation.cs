using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float moveTime = 3f;
    public float restTime = 1f;

    [HideInInspector]
    public GameObject tracker;

    void Awake()
    {
        tracker = new GameObject("Tracker");
        tracker.transform.parent = this.transform.parent;
        tracker.transform.position = pointA.position;
        StartCoroutine(loop());
    }

    void Update()
    {
        this.transform.LookAt(tracker.transform);
    }

    IEnumerator loop(){
        yield return StartCoroutine(lookAt(pointA.position, pointB.position, moveTime));
        yield return StartCoroutine(wait(restTime));
        yield return StartCoroutine(lookAt(pointB.position, pointA.position, moveTime));
        yield return StartCoroutine(wait(restTime));
        yield return StartCoroutine(loop());
    }

    IEnumerator lookAt(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            tracker.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        tracker.transform.position = targetPosition;
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
