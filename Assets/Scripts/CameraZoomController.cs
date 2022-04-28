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
    //private Cinemachine3rdPersonFollow _3rdPersonFollow;
    private CinemachineFramingTransposer _framingTransposer;
    // Start is called before the first frame update
    void Start()
    {
        _framingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _defaultAreaCameraDistance = _framingTransposer.m_CameraDistance;
        _defaultVerticalArmLength = _framingTransposer.m_ScreenY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SetCameraDistance(float distance, float verticalArmLength, float duration)
    {
        float startDistance = _framingTransposer.m_CameraDistance;
        float endDistance = distance;
        float startVerticalArmLength = _framingTransposer.m_ScreenY;
        float endVerticalArmLength = verticalArmLength;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            _framingTransposer.m_CameraDistance = Mathf.Lerp(startDistance, endDistance, f);
            _framingTransposer.m_ScreenY = Mathf.Lerp(startVerticalArmLength, endVerticalArmLength, f);  
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
