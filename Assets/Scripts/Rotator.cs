using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    private Vector3[] _degrees;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private GameObject[] _objects;

    [SerializeField]
    private float _initialDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action() {
        for (int i = 0; i < _objects.Length; i++) {
            StartCoroutine(Rotate(_objects[i], _degrees[i], _duration));
        }
    }

    private IEnumerator Rotate(GameObject obj, Vector3 rotation, float duration)
    {
        Quaternion startPosition = obj.transform.rotation;
        Quaternion endPosition = Quaternion.Euler(startPosition.x + rotation.x, startPosition.x + rotation.y, startPosition.x + rotation.z);
        yield return new WaitForSeconds(_initialDelay);
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            obj.transform.rotation = Quaternion.Lerp(startPosition, endPosition, f);
            yield return null;
        }
    }

}
