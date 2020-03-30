using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionConfigurator : AIStateMachineLink
{

    [SerializeField] private int rootPosition_ = 0;
    [SerializeField] private int rootRotation_ = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateMachine_)
        {
            //Debug.Log(stateMachine_.GetType().ToString());
            stateMachine_.AddRootMotionRequest(rootPosition_, rootRotation_);
        }
                 
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateMachine_)
            stateMachine_.AddRootMotionRequest(- rootPosition_, - rootRotation_);
        
    }

}
