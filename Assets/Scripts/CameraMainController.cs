using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMainController : MonoBehaviour
{
    public CinemachineVirtualCamera _vcam;
    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    [SerializeField]
    private float _defaultCameraDistance = 8;
    [SerializeField]
    private float _defaultVerticalArmLength = 0.66f;
    [SerializeField]
    private float _defaultScreenX = 0.5f;
    [SerializeField]
    private Vector3 _defaultCameraRotation = Vector3.zero;
    [SerializeField]
    private float _defaultNoiseFrequencyGain = 0.3f;
    [SerializeField]
    private float _defaultAmplitudGain = 0.5f;
    [SerializeField]
    private float _defaultDampingX = 1f;
    [SerializeField]
    private float _defaultDampingY = 1f;

    void Start()
    {
        _framingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _multiChannelPerlin = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _defaultCameraDistance = _framingTransposer.m_CameraDistance;
        _defaultVerticalArmLength = _framingTransposer.m_ScreenY;
    }

    public void CameraMovementEnter(CameraZoomController camera)
    {
        StopAllCoroutines();
        StartCoroutine(DoCameraMovement(camera._areaCameraDistance, camera._areaCameraVerticalArmLength, camera._areaScreenX, camera._areaCameraRotation, camera._areaNoiseFrequencyGain, camera._areaAmplitudGain, camera._areaDampingX, camera._areaDampingY, camera._enterTransitionSeconds));
    }

    public void CameraMovementExit(CameraZoomController camera)
    {
        StopAllCoroutines();
        StartCoroutine(DoCameraMovement(_defaultCameraDistance, _defaultVerticalArmLength, _defaultScreenX, _defaultCameraRotation, _defaultNoiseFrequencyGain, _defaultAmplitudGain, _defaultDampingX, _defaultDampingY, camera._exitTransitionSeconds));
    }

    private IEnumerator DoCameraMovement(float distance, float verticalArmLength, float screenX, Vector3 cameraRotation, float noiseFrequencyGain, float noiseAmplitudGain, float dampingX, float dampingY, float duration)
    {
        float startDistance = _framingTransposer.m_CameraDistance;
        float endDistance = distance;

        float startVerticalArmLength = _framingTransposer.m_ScreenY;
        float endVerticalArmLength = verticalArmLength;

        float startScreenX = _framingTransposer.m_ScreenX;
        float endScreenX= screenX;

        float startNoiseFrequencyGain = _multiChannelPerlin.m_FrequencyGain;
        float endNoiseFrequencyGain = noiseFrequencyGain;

        float startNoiseAmplitudGain = _multiChannelPerlin.m_AmplitudeGain;
        float endNoiseAmplitudGain = noiseAmplitudGain;

        float startDampingX = _framingTransposer.m_XDamping;
        float endDampingX = dampingX;

        float startDampingY = _framingTransposer.m_XDamping;
        float endDampingY = dampingY;

        Quaternion startRotation = _vcam.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.x + cameraRotation.x, startRotation.y + cameraRotation.y, startRotation.z + cameraRotation.z);

        if (duration != 0) { 
            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                float x = Mathf.Clamp01(t / duration);
                float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
                _framingTransposer.m_CameraDistance = Mathf.Lerp(startDistance, endDistance, f);
                _framingTransposer.m_ScreenY = Mathf.Lerp(startVerticalArmLength, endVerticalArmLength, f);
                _framingTransposer.m_ScreenX = Mathf.Lerp(startScreenX, endScreenX, f);
                _framingTransposer.m_XDamping = Mathf.Lerp(startDampingX, endDampingX, f);
                _framingTransposer.m_YDamping = Mathf.Lerp(startDampingY, endDampingY, f);
                _vcam.gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, f);
                _multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(startNoiseFrequencyGain, endNoiseFrequencyGain, f);
                _multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startNoiseAmplitudGain, endNoiseAmplitudGain, f);
                yield return null;
            }
        }

        _framingTransposer.m_CameraDistance = distance;
        _framingTransposer.m_ScreenX = endScreenX;
        _framingTransposer.m_ScreenY = verticalArmLength;
        _framingTransposer.m_XDamping = endDampingX;
        _framingTransposer.m_YDamping = endDampingY;
        _vcam.gameObject.transform.rotation = Quaternion.Euler(startRotation.x + cameraRotation.x, startRotation.y + cameraRotation.y, startRotation.z + cameraRotation.z); ;
        _multiChannelPerlin.m_FrequencyGain = noiseFrequencyGain;
        _multiChannelPerlin.m_AmplitudeGain = noiseAmplitudGain;
        yield return null;

    }
}
