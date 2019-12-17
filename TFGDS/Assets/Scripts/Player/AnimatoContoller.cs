using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatoContoller : MonoBehaviour
{
    public GameObject model;
    public UserInput pi;
    public float walkSpeed = 1.4f;
    public float runMultiplier;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;

    [Header(" ===== friction setting======")]
    public PhysicMaterial FrictionOne;
    public PhysicMaterial FrictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVect; // vector de movimiento
    private Vector3 thrustVect; // vector de impulso
    private bool canAttack;
    private CapsuleCollider col;
    private float lerpTarget;
    private Vector3 deltaPos;

    private bool lockMoving = false;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<UserInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() // 1 / 60
    {
         AnimationUpdate();

        if(lockMoving == false)
        {
            // vector  que alamacena la veclocidad 
            movingVect = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
        }
          
    }
    // metodos para actualizar las animaciones y los movimeintos
    private void AnimationUpdate()
    {
        float targetRunMult = ((pi.run) ? 2.0f : 1.0f);
        //                                  The interpolated float result between the two float values
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMult, 0.5f));

        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f) ; //interpolates between two vectors.
            // giro del personajes
            model.transform.forward = targetForward;
           
        }

        // jump personaje
        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        //roll state
        if(rigid.velocity.magnitude > 0f)
        {
            anim.SetTrigger("roll");
        }

        //attack
        if (pi.attack && canAttack && CheckState("ground"))
        {
            anim.SetTrigger("attack");
        }

        // defense
        anim.SetBool("defense", pi.defense);
        //anim.SetBool("defense", true);
        // print(CheckState("idle", "attack"));
    }

    private void FixedUpdate()  // 1 / 50
    {
        rigid.position += deltaPos; // OnAnimatorUpdateRM y fixedUpdate no es de mismo tiempo
        //rigid.position += movingVect * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(movingVect.x, rigid.velocity.y, movingVect.z) + thrustVect; // añadoir velocidad al personaje
        thrustVect = Vector3.zero; // for impulso
        deltaPos = Vector3.zero;
    }

    public bool CheckState(string stateName, string layerName = "Base Layer" )
    {
        int layerIndex_ = anim.GetLayerIndex(layerName);
        bool output = anim.GetCurrentAnimatorStateInfo(layerIndex_).IsName(stateName);
        return output;
    }

    /// <summary>
    /// Jump
    /// </summary>
    public void OnJumpEnter()
    {
        pi.inputEnable = false;
        lockMoving = true;
        thrustVect = new Vector3(0, jumpVelocity, 0);
    }
    /// <summary>
    /// Ground
    /// </summary>
    public void IsGround()
    {
        //print("Ground");
        anim.SetBool("isGround", true);
        
    }

    public void IsNotGround()
    {
        // print("Not Ground");
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockMoving = false;
        canAttack = true;
        col.material = FrictionOne;
    }

    public void OnGroundExit()
    {
        col.material = FrictionZero;
    }

    /// <summary>
    /// Fall animation 
    /// </summary>
    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockMoving = true;
    }
    /// <summary>
    /// Roll anim
    /// </summary>
    public void OnRollEnter()
    {
        pi.inputEnable = false;
        lockMoving = true ;
        //thrustVect = new Vector3(rollVelocity , 0, 0);
    }

    public void OnRollUpdate()
    {
        thrustVect = model.transform.forward * (rollVelocity);
    }
    /// <summary>
    /// attack layer Anim
    /// </summary>
    public void OnAttack1hAEnter()
    {
        pi.inputEnable = false;
        //lockMoving = true;
        lerpTarget = 1.0f;
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 1.0f);
    }

    public void OnAttackIdle()
    {
        pi.inputEnable = true;
        //lockMoving = false;
        lerpTarget = 0;
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0);
    }

    public void OnAttack1AUpdate()
    {
        thrustVect = model.transform.forward * anim.GetFloat("attackVelocity"); // ataque avanza
        /*float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack")); // cambio de weight con interpolacion
		currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
		anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);*/
        

    }

    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack")); // cambio de peso con interpolacion
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        //print(currentWeight)
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), lerpTarget);

    }

    //
    public void OnAnimatorUpdateRM(Vector3 animDeltaPos)
    {
        //print(deltaPos);
        if(CheckState("attack1hC", "attack") || (CheckState("roll")))
        {
            thrustVect = Vector3.zero;
            deltaPos += animDeltaPos;
        }
        
    }
}
