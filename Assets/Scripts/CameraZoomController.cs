using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    public float _areaCameraDistance = 10;
    public float _areaCameraVerticalArmLength = 1.25f;
    private float _defaultAreaCameraDistance = 8;
    private float _defaultVerticalArmLength;
    public float _transitionSeconds = 2;
    public CinemachineVirtualCamera _vcam;
    private Cinemachine3rdPersonFollow _3rdPersonFollow;
    // Start is called before the first frame update
    void Start()
    {
        _3rdPersonFollow = _vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _defaultAreaCameraDistance = _3rdPersonFollow.CameraDistance;
        _defaultVerticalArmLength = _3rdPersonFollow.VerticalArmLength;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SetCameraDistance(float distance, float verticalArmLength, float duration)
    {
        float startDistance = _3rdPersonFollow.CameraDistance;
        float endDistance = distance;
        float startVerticalArmLength = _3rdPersonFollow.VerticalArmLength;
        float endVerticalArmLength = verticalArmLength;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            _3rdPersonFollow.CameraDistance = Mathf.Lerp(startDistance, endDistance, f);
            _3rdPersonFollow.VerticalArmLength = Mathf.Lerp(startVerticalArmLength, endVerticalArmLength, f);  
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            StartCoroutine(SetCameraDistance(_areaCameraDistance, _areaCameraVerticalArmLength, _transitionSeconds));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SetCameraDistance(_defaultAreaCameraDistance, _defaultVerticalArmLength, _transitionSeconds));
        }
    }

}
