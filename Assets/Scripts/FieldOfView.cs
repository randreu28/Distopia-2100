using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private Transform _pointOfView;
    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetLayer;
    public LayerMask obstaclesLayer;

    public bool inFOV;

    public Transform objectInFOV;

    

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] collisionsInRadius = Physics.OverlapSphere(_pointOfView.position, radius, targetLayer);

        if (collisionsInRadius.Length != 0)
        {
            Transform target = collisionsInRadius[0].transform;
            Vector3 directionToCollider = (target.position - _pointOfView.position).normalized;

            if (Vector3.Angle(_pointOfView.forward, directionToCollider) < angle / 2)
            {
                float distanceToCollider = Vector3.Distance(_pointOfView.position, target.position);

                if (!Physics.Raycast(_pointOfView.position, directionToCollider, distanceToCollider, obstaclesLayer))
                {
                    inFOV = true;
                    objectInFOV = target;
                    BroadcastMessage("Detected", objectInFOV);
                }
                else
                {
                    inFOV = false;
                    objectInFOV = null;
                }

            }
            else
            {
                inFOV = false;
                objectInFOV = null;
            }
        }
        else if (inFOV) {
            inFOV = false;
            objectInFOV = null;
        }
    }
}