using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2 )]
public class PatrolState : AbstractFSMState
{
    NPCPatrolPoint[] _patrolPoints;
    int _patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        _patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnteredState = false;
        if (base.EnterState())
        {
            _patrolPoints = _npc._patrolPoints;

            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {
                Debug.LogError("PatrolState: Failed to grab patrol points from the NPC");
            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    //_patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                    _patrolPointIndex = 0;
                }
                else
                {
                    _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                }

                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnteredState = true;
                //Debug.Log("PATROL EnterState()");
            }

        }

        return EnteredState;
    }        

    public override void UpdateState()
    {
        if (EnteredState) {
            if (Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <= 1f) {
                _fsm.EnterState(FSMStateType.IDLE);
            }
            if (_fieldOfView.inFOV)
            {
                _fsm.EnterState(FSMStateType.CHASE);
            }
        }
    }

    private void SetDestination(NPCPatrolPoint destination)
    {
        //Debug.Log("PATROL STATE -> SetDestination()");
        if (_navMeshAgent != null && destination != null) {
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

}