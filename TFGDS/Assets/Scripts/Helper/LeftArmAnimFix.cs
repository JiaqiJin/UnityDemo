using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{

    private Animator anim;

    public Vector3 vect;
    public Vector3 vect2;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex) // cinematica inversa
    {
        if(anim.GetBool("defense") == false)
        {
            Transform leftLoweArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLoweArm.localEulerAngles += vect;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLoweArm.localEulerAngles));
        }
        else if(anim.GetBool("defense") == true)
        {
            Transform leftLoweArm2 = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLoweArm2.localEulerAngles += vect2;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLoweArm2.localEulerAngles));
        }
        //leftLoweArm = GameObject.Find("");
       
    }
}
