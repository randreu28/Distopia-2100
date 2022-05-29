using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFallingDistanceChanger : MonoBehaviour
{

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private float _maxFallingDistance;

    private float _default;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.SetMaxFallingDistance(_maxFallingDistance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.SetDefaultMaxFallingDistance();
        }
    }
}
