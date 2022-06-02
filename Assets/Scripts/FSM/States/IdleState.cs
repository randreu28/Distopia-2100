using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/States/Idle", order =1)]

public class IdleState : AbstractFSMState
{
    [SerializeField]
    float _idleDuration = 3f;

    float _totalDuration;

    public override void OnEnable()
    {
            

        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
            
        EnteredState = base.EnterState();

        if (EnteredState) { 
                
            _totalDuration = 0f;
        }
        //Debug.Log("IDLE EnterState()");
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState) {
            _totalDuration += Time.deltaTime;
            //Debug.Log("UPDATING IDLE STATE");
            if (_totalDuration >= _idleDuration) {
                _fsm.EnterState(FSMStateType.PATROL);
            }
            if (_fieldOfView.inFOV)
            {
                _fsm.EnterState(FSMStateType.CHASE);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}