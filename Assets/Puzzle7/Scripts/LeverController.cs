using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    //private bool _isPlayerClose;
    private bool _isLightOn;
    public Transform _switch;
    public int SwitchRotation = 20;
    public int _animationTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        //_isPlayerClose = false;
        _isLightOn = true;
        //_switch = transform.Find("Switch").Find("Pivot");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleLight()
    {
        if (_isLightOn)
        {
            StopAllCoroutines();
            StartCoroutine(SetSwitcherPosition(SwitchRotation, _animationTime));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(SetSwitcherPosition(-SwitchRotation, _animationTime));
        }
        _isLightOn = !_isLightOn;
    }

    private IEnumerator SetSwitcherPosition(float rotation, float duration)
    {
        Quaternion startPosition = _switch.transform.rotation;
        Quaternion endPosition = Quaternion.Euler(0, 0, rotation);
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            _switch.transform.rotation = Quaternion.Lerp(startPosition, endPosition, f);
            yield return null;
        }
    }

    /*private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().name == "Capsule")
        {
            _isPlayerClose = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<Collider>().name == "Capsule")
        {
            _isPlayerClose = false;
        }
    }*/
}
