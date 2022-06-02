using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-FSM/States/Chase", order = 3 )]
public class ChaseState : AbstractFSMState
{

    private Vector3 _lastDestination;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.CHASE;
    }

    public override bool EnterState()
    {
        //Debug.Log("ENTERING CHASE STATE");
        EnteredState = false;
        if (base.EnterState())
        {
            EnteredState = true;
        }
        return EnteredState;
    }        

    public override void UpdateState()
    {
        if (EnteredState) {
            if (_fieldOfView.inFOV)
            {
                SetDestination(_fieldOfView.objectInFOV);
                if (Vector3.Distance(_navMeshAgent.transform.position, _fieldOfView.objectInFOV.transform.position) <= 1f)
                {
                    //Debug.Log("Kill The Player");
                }
            }
            else
            {
                if (_npc.IdleAfterChase)
                    _fsm.EnterState(FSMStateType.IDLE);
                else
                    _fsm.EnterState(FSMStateType.PATROL);
            }
        }
    }

    private void SetDestination(Transform destination)
    {
        if (_navMeshAgent != null && destination != null) {
            _navMeshAgent.SetDestination(destination.position);
            _lastDestination = destination.position;
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

}