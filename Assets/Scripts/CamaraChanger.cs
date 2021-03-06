using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamaraChanger : MonoBehaviour
{
    [SerializeField]
    private float _areaCameraDistance = 10;
    [SerializeField]
    private float _areaCameraVerticalArmLength = 1.25f;
    [SerializeField]
    private Vector3 _areaCameraRotation;

    [SerializeField]
    private float _areaNoiseFrequencyGain = 0.3f;

    [SerializeField]
    private float _areaAmplitudGain = 2f;

    [SerializeField]
    private float _areaFieldOfView;

    private float _defaultAreaCameraDistance = 8;
    private float _defaultVerticalArmLength = 0.66f;
    private Vector3 _defaultAreaCameraRotation = Vector3.zero;
    //private float _defaultAreaNoiseFrequencyGain = 0.3f;
    //private float _defaultAreaAmplitudGain = 0.5f;

    public float _transitionSeconds = 2;
    public CinemachineVirtualCamera _vcam;
    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    

    [SerializeField]
    private GameObject[] _CamaraZonesToDisableOnAction;

    void Start()
    {
        _framingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _multiChannelPerlin = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _defaultAreaCameraDistance = _framingTransposer.m_CameraDistance;
        _defaultVerticalArmLength = _framingTransposer.m_ScreenY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SetCameraDistance(float distance, float verticalArmLength, Vector3 cameraRotation, float noiseFrequencyGain, float noiseAmplitudGain, float fieldOfView, float duration)
    {
        float startDistance = _framingTransposer.m_CameraDistance;
        float endDistance = distance;

        float startVerticalArmLength = _framingTransposer.m_ScreenY;
        float endVerticalArmLength = verticalArmLength;

        float startNoiseFrequencyGain = _multiChannelPerlin.m_FrequencyGain;
        float endNoiseFrequencyGain = noiseFrequencyGain;

        float startNoiseAmplitudGain = _multiChannelPerlin.m_AmplitudeGain;
        float endNoiseAmplitudGain = noiseAmplitudGain;

        Quaternion startRotation = _vcam.gameObject.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.x + cameraRotation.x, startRotation.y + cameraRotation.y, startRotation.z + cameraRotation.z);

        float startFieldOfView = _vcam.m_Lens.FieldOfView;
        float endFieldOfView = fieldOfView;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            _framingTransposer.m_CameraDistance = Mathf.Lerp(startDistance, endDistance, f);
            _framingTransposer.m_ScreenY = Mathf.Lerp(startVerticalArmLength, endVerticalArmLength, f);
            _vcam.gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, f);
            _multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(startNoiseFrequencyGain, endNoiseFrequencyGain, f);
            _multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startNoiseAmplitudGain, endNoiseAmplitudGain, f);
            _vcam.m_Lens.FieldOfView = Mathf.Lerp(startFieldOfView, endFieldOfView, f);
            yield return null;
        }
    }

    private void Action() {
        for (int i = 0; i < _CamaraZonesToDisableOnAction.Length; i++) {
            _CamaraZonesToDisableOnAction[i].SetActive(false);
        }
        StartCoroutine(SetCameraDistance(_areaCameraDistance, _areaCameraVerticalArmLength, _areaCameraRotation, _areaNoiseFrequencyGain, _areaAmplitudGain, _areaFieldOfView, _transitionSeconds));
    }
}
