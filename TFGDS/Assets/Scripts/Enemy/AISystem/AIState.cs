using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    //public methos
    public void SetStateMachine(AIStateMachine statemachine) { statemachine_ = statemachine; }

    //default 
    public virtual void OnEnterState() { }
    public virtual void OnExitState() { }
    public virtual void OnAnimatorUpdate()
    {
        if (statemachine_.useRootPosition)
            statemachine_.navAgent.velocity = statemachine_.animator.deltaPosition / Time.deltaTime;

        if (statemachine_.useRootRotation)
            statemachine_.transform.rotation = statemachine_.animator.rootRotation;

    }

    public virtual void OnAnimatorIKUpdate() { }
    public virtual void OnTriggerEvent(AITriggerEventType eventType, Collider other) { }
    public virtual void OnDestinationReached(bool isReached) { }


    //metosods abstractos
    public abstract AIStateType GetStateType();
    public abstract AIStateType OnUpdate();

    protected AIStateMachine statemachine_;
}
