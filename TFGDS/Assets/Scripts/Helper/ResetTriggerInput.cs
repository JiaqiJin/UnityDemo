using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggerInput : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ResetTrigger(string trigerName)
    {
        //print(trigerName);
        anim.ResetTrigger(trigerName);
    }
}
