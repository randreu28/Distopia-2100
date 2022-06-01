using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFieldOfView : MonoBehaviour
{

    [SerializeField]
    private Transform _player;

    [SerializeField]
    public Transform _pointOfView;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetLayer;
    public LayerMask obstaclesLayer;

    public bool inFOV;

    public Transform objectInFOV;    

    private void Start()
    {
    }

    private void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(_pointOfView.position, radius, targetLayer);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - _pointOfView.position).normalized;

            if (Vector3.Angle(_pointOfView.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(_pointOfView.position, target.position);

                RaycastHit hit;

                if (!Physics.Raycast(_pointOfView.position, directionToTarget, out hit, distanceToTarget, obstaclesLayer))
                {
                    inFOV = true;
                    objectInFOV = _player;
                }
                else
                {
                    inFOV = false;
                    objectInFOV = null;
                }
            }
            else { 
                inFOV = false;
                objectInFOV = null;
            }
        }
    }
}