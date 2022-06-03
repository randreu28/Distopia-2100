using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField]
    public float _areaCameraDistance = 10;

    [SerializeField]
    public float _areaScreenX = 0.5f;

    [SerializeField]
    public float _areaCameraVerticalArmLength = 1.25f;

    [SerializeField]
    public Vector3 _areaCameraRotation;

    [SerializeField]
    public float _areaNoiseFrequencyGain = 0.3f;

    [SerializeField]
    public float _areaAmplitudGain = 2f;

    [SerializeField]
    public bool _enterTransition;

    [SerializeField]
    public float _enterTransitionSeconds = 2;

    [SerializeField]
    public bool _exitTransition;

    [SerializeField]
    public float _exitTransitionSeconds = 2;

    [SerializeField]
    public bool _justOnce;
    public bool _entered;

    [SerializeField]
    public bool _useCamaraRotation;

    [SerializeField]
    public float _areaDampingX = 1f;

    [SerializeField]
    public float _areaDampingY = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (_enterTransition) {
            if (other.tag == "Player" && (!_justOnce || (_justOnce && !_entered))) {
                other.GetComponent<PlayerController>()._useCameraRotation = _useCamaraRotation;
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
                SendMessageUpwards("CameraMovementExit", this);
            }
        }
    }

}
