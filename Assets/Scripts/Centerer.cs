using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centerer: MonoBehaviour
{

    [SerializeField]
    private float _duration;

    [SerializeField]
    private bool _infinity;

    [SerializeField]
    private bool _ignoreY;

    [SerializeField]
    private float _speed;

    private Rigidbody _player;

    [SerializeField]
    private float _initialDelay;
    private float counter;

    private void Start()
    {
        counter = Time.time;
    }

    void Update()
    {
        if (_infinity && _player != null) {
            if (counter > _initialDelay)
            {
                Vector3 newPosition = Vector3.MoveTowards(_player.position, transform.position, _speed * Time.deltaTime);
                if (_ignoreY)
                {
                    _player.MovePosition(new Vector3(newPosition.x, _player.position.y, newPosition.z));
                }
                else
                {
                    _player.MovePosition(newPosition);
                }
            }
            else {
                counter += Time.deltaTime;
            }
        }
    }

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
            _player = other.GetComponent<Rigidbody>();
            Debug.Log("Player position: " + other.transform.position);
            Debug.Log("Platform position: " + transform.position);
            if (!_infinity) { 
                StartCoroutine(SetPosition(other.transform, _duration));
            }
        }
    }

}
