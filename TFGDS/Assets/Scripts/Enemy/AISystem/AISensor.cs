using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    private AIStateMachine parentStateMachine_ = null;
    public AIStateMachine parentStateMachine
    {
        set { parentStateMachine_ = value; }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (parentStateMachine_ != null)
            parentStateMachine_.OnTriggerEvent(AITriggerEventType.Enter, col);
    }

    private void OnTriggerStay(Collider col)
    {
        if (parentStateMachine_ != null)
            parentStateMachine_.OnTriggerEvent(AITriggerEventType.Stay, col);
    }


    private void OnTriggerExit(Collider col)
    {
        if (parentStateMachine_ != null)
            parentStateMachine_.OnTriggerEvent(AITriggerEventType.Exit, col);
    }
}
