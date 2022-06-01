using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField]
    public float _areaCameraDistance = 10;
    [SerializeField]
    public float _areaCameraVerticalArmLength = 1.25f;
    [SerializeField]
    public Vector3 _areaCameraRotation;

    [SerializeField]
    public float _areaNoiseFrequencyGain = 0.3f;

    [SerializeField]
    public float _areaAmplitudGain = 2f;

    public float _defaultAreaCameraDistance = 8;
    public float _defaultVerticalArmLength = 0.66f;
    public Vector3 _defaultAreaCameraRotation = Vector3.zero;
    public float _defaultAreaNoiseFrequencyGain = 0.3f;
    public float _defaultAreaAmplitudGain = 0.5f;

    [SerializeField]
    public bool _enterTransition;

    [SerializeField]
    public float _enterTransitionSeconds = 2;

    [SerializeField]
    public bool _exitTransition;

    [SerializeField]
    public float _exitTransitionSeconds = 2;

    public CinemachineVirtualCamera _vcam;
    public CinemachineFramingTransposer _framingTransposer;
    public CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    [SerializeField]
    public bool _justOnce;
    public bool _entered;

    [SerializeField]
    public bool _useCamaraRotation;


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

    private IEnumerator SetCameraDistance(float distance, float verticalArmLength, Vector3 cameraRotation, float noiseFrequencyGain, float noiseAmplitudGain, float duration)
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

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            _framingTransposer.m_CameraDistance = Mathf.Lerp(startDistance, endDistance, f);
            _framingTransposer.m_ScreenY = Mathf.Lerp(startVerticalArmLength, endVerticalArmLength, f);
            _vcam.gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, f);
            _multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(startNoiseFrequencyGain, endNoiseFrequencyGain, f);
            _multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startNoiseAmplitudGain, endNoiseAmplitudGain, f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enterTransition) {
            if (other.tag == "Player" && (!_justOnce || (_justOnce && !_entered))) {
                other.GetComponent<PlayerController>()._useCameraRotation = _useCamaraRotation;
                //Debug.Log("Player Enter Camera Zone");
                //StopAllCoroutines();
                //StartCoroutine(SetCameraDistance(_areaCameraDistance, _areaCameraVerticalArmLength, _areaCameraRotation, _areaNoiseFrequencyGain, _areaAmplitudGain, _enterTransitionSeconds));
                _entered = true;
                SendMessageUpwards("CameraMovementEnter", this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_exitTransition)
        {
            if (other.tag == "Player")
            {
                BroadcastMessage("CameraMovementExit", this);
                //Debug.Log("Player Exit Camera Zone");
                //StopAllCoroutines();
                //StartCoroutine(SetCameraDistance(_defaultAreaCameraDistance, _defaultVerticalArmLength, _defaultAreaCameraRotation, _defaultAreaNoiseFrequencyGain, _defaultAreaAmplitudGain, _exitTransitionSeconds));
            }
        }
    }

}
