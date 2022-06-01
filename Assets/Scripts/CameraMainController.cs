using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMainController : MonoBehaviour
{
    public CinemachineVirtualCamera _vcam;
    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    void Start()
    {
        _framingTransposer = _vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _multiChannelPerlin = _vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CameraMovementEnter(CameraZoomController camera)
    {
        StopAllCoroutines();
        StartCoroutine(DoCameraMovement(camera, camera._areaCameraDistance, camera._areaCameraVerticalArmLength, camera._areaCameraRotation, camera._areaNoiseFrequencyGain, camera._areaAmplitudGain, camera._enterTransitionSeconds));
    }

    public void CameraMovementExit(CameraZoomController camera)
    {
        StopAllCoroutines();
        StartCoroutine(DoCameraMovement(camera, camera._defaultAreaCameraDistance, camera._defaultVerticalArmLength, camera._defaultAreaCameraRotation, camera._defaultAreaNoiseFrequencyGain, camera._defaultAreaAmplitudGain, camera._exitTransitionSeconds));
    }

    private IEnumerator DoCameraMovement(CameraZoomController camera, float distance, float verticalArmLength, Vector3 cameraRotation, float noiseFrequencyGain, float noiseAmplitudGain, float duration)
    {
        float startDistance = _framingTransposer.m_CameraDistance;
        float endDistance = distance;

        float startVerticalArmLength = _framingTransposer.m_ScreenY;
        float endVerticalArmLength = verticalArmLength;

        float startNoiseFrequencyGain = _multiChannelPerlin.m_FrequencyGain;
        float endNoiseFrequencyGain = noiseFrequencyGain;

        float startNoiseAmplitudGain = _multiChannelPerlin.m_AmplitudeGain;
        float endNoiseAmplitudGain = noiseAmplitudGain;

        Quaternion startRotation = _vcam.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.x + cameraRotation.x, startRotation.y + cameraRotation.y, startRotation.z + cameraRotation.z);

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            float x = Mathf.Clamp01(t / duration);
            float f = 3 * Mathf.Pow(x, 2) - 2 * Mathf.Pow(x, 3);
            camera._framingTransposer.m_CameraDistance = Mathf.Lerp(startDistance, endDistance, f);
            camera._framingTransposer.m_ScreenY = Mathf.Lerp(startVerticalArmLength, endVerticalArmLength, f);
            camera._vcam.gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, f);
            camera._multiChannelPerlin.m_FrequencyGain = Mathf.Lerp(startNoiseFrequencyGain, endNoiseFrequencyGain, f);
            camera._multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startNoiseAmplitudGain, endNoiseAmplitudGain, f);
            yield return null;
        }
    }
}
