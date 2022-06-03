using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerer: MonoBehaviour
{

    [SerializeField]
    private float _duration;


    private IEnumerator SetPosition(Transform obj, float duration)
    {
        //yield return new WaitForSeconds(_initialDelay);
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            Vector3 startPosition = obj.position;
            Vector3 endPosition = new Vector3(transform.position.x, obj.position.y, transform.position.z);
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player position: " + other.transform.position);
            Debug.Log("Platform position: " + transform.position);
            StartCoroutine(SetPosition(other.transform, _duration));
        }
    }

}
