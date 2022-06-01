using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine), typeof(NPCFieldOfView))]
    
public class NPC : MonoBehaviour
{
    [SerializeField]
    public NPCPatrolPoint[] _patrolPoints;

    [SerializeField]
    public bool IdleAfterChase;

    NavMeshAgent _navMeshAgent;
    FiniteStateMachine _finiteStateMachine;
    NPCFieldOfView _fieldOfView;

    public void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _finiteStateMachine = GetComponent<FiniteStateMachine>();
        _fieldOfView = GetComponent<NPCFieldOfView>();
    }

    public void Start()
    {

    }

    public void Update()
    {

    }

    public NPCPatrolPoint[] PatrolPoints {
        get {
            return PatrolPoints;
        }
    }

    public void Reset()
    {
        _navMeshAgent.transform.position = _patrolPoints[0].transform.position;
    }

}