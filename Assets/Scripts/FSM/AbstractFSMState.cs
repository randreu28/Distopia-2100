using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState {
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
}

public enum FSMStateType {
    IDLE,
    PATROL,
    CHASE,
}

public abstract class AbstractFSMState : ScriptableObject
{

    protected NavMeshAgent _navMeshAgent;
    protected NPC _npc;
    protected FiniteStateMachine _fsm;
    protected NPCFieldOfView _fieldOfView;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }

    public bool EnteredState { get; protected set; }

    public virtual void OnEnable() {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState() {

        bool successNavMesh = true;
        bool successNPC = true;
        bool successFieldOfView = true;

        ExecutionState = ExecutionState.ACTIVE;
        successNavMesh = (_navMeshAgent != null);
        successNPC = (_npc != null);
        successFieldOfView = (_fieldOfView != null);

        return successNavMesh & successNPC & successFieldOfView;
    }

    public abstract void UpdateState();

    public virtual bool ExitState() {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent navMeshAgent) {
        if (navMeshAgent != null) {
            _navMeshAgent = navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if (fsm != null)
        {
            _fsm = fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC npc) {
        if (npc != null) {
            _npc = npc;
        }
    }

    public virtual void SetFieldOfView(NPCFieldOfView fieldOfView)
    {
        if (fieldOfView != null)
        {
            _fieldOfView = fieldOfView;
        }
    }

}
