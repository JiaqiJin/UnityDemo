using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RootMontionControl : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        //Vector3 newPosition = anim.transform.position;
        SendMessageUpwards("OnAnimatorUpdateRM",anim.deltaPosition);
    }
}
