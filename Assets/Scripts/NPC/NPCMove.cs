using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [SerializeField]
    private Transform _destination;

    NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.Log("The Nav Mesh Agent is not attached to " + gameObject.name);
        }
        else {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (_destination != null) {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }
}
