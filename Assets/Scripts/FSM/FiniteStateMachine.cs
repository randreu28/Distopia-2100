using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FiniteStateMachine : MonoBehaviour
{
    AbstractFSMState _currentState;

    [SerializeField]
    List<AbstractFSMState> _validStates;

    Dictionary<FSMStateType, AbstractFSMState> _fsmStates;

    public void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        NPC npc = GetComponent<NPC>();
        NPCFieldOfView fieldOfView = GetComponent<NPCFieldOfView>();

        foreach (AbstractFSMState state in _validStates) {

            var instance = Instantiate(state);

            instance.SetExecutingFSM(this);
            instance.SetExecutingNPC(npc);
            instance.SetNavMeshAgent(navMeshAgent);
            instance.SetFieldOfView(fieldOfView);
            _fsmStates.Add(state.StateType, instance);
        }
    }

    public void Start()
    {
        EnterState(FSMStateType.IDLE);
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
        {
            return;
        }

        if (_currentState != null) {
            _currentState.ExitState();
        }

        _currentState = nextState;
        _currentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        //Debug.Log("FSMStateType: " + stateType);
        if (_fsmStates.ContainsKey(stateType)) {
            AbstractFSMState nextState = _fsmStates[stateType];
            EnterState(nextState);
        }
    }
}