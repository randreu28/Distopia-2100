using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField]
    private Vector3[] _RelativeNewPosition;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private GameObject[] _objects;

    [SerializeField]
    private float _initialDelay;

    [SerializeField]
    private bool _doReverseOnStart;

    [SerializeField]
    private GameObject[] _objectsToDestroyAfterReverse;

    // Start is called before the first frame update
    void Start()
    {
        if (_doReverseOnStart) { 
            for (int i = 0; i < _objects.Length; i++)
            {
                StartCoroutine(Rotate(_objects[i], -_RelativeNewPosition[i], _duration));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Action()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            StartCoroutine(Rotate(_objects[i], _RelativeNewPosition[i], _duration));
        }
    }

    private IEnumerator Rotate(GameObject obj, Vector3 relativePosition, float duration)
    {
        Vector3 startPosition = obj.transform.position;
        Vector3 endPosition = startPosition + relativePosition;
        yield return new WaitForSeconds(_initialDelay);
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, f);
            yield return null;
        }

        if (_doReverseOnStart) {
            for (int i = 0; i < _objects.Length; i++)
            {
                Destroy(_objectsToDestroyAfterReverse[i]);
            }
            _doReverseOnStart = false;
        }
    }

}
