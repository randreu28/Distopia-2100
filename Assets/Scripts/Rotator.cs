using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    private bool _absoluteDegrees;

    [SerializeField]
    private Vector3[] _degrees;

    [SerializeField]
    private float[] _duration;

    [SerializeField]
    private GameObject[] _objects;

    [SerializeField]
    private float _initialDelay;

    [SerializeField]
    private bool _onAction = true;

    [SerializeField]
    private bool _onTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action() {
        if (_onAction) { 
            for (int i = 0; i < _objects.Length; i++) {
                StartCoroutine(Rotate(_objects[i], _degrees[i], _duration[i]));
            }
        }
    }

    private IEnumerator Rotate(GameObject obj, Vector3 rotation, float duration)
    {
        yield return new WaitForSeconds(_initialDelay);

        Quaternion startPosition = obj.transform.rotation;
        Quaternion endPosition;
        if (!_absoluteDegrees)
        {
            endPosition = Quaternion.Euler(startPosition.x + rotation.x, startPosition.x + rotation.y, startPosition.x + rotation.z);
        }
        else
        {
            endPosition = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        }

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            obj.transform.rotation = Quaternion.Lerp(startPosition, endPosition, f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_onTrigger) { 
            if (other.tag == "Player") {
                for (int i = 0; i < _objects.Length; i++)
                {
                    StartCoroutine(Rotate(_objects[i], _degrees[i], _duration[i]));
                }
            }
        }
    }

}
