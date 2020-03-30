using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animador crea una nueva instancia de cada comportamiento definido en el controlador. 
/// </summary>
public class AIStateMachineLink : StateMachineBehaviour
{
    protected AIStateMachine stateMachine_;
    public AIStateMachine stateMachine { set { stateMachine_ = value; } }
}
