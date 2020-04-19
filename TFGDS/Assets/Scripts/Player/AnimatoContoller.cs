using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatoContoller : MonoBehaviour
{
    public GameObject model;
    public camaraContoller camcom;
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
    private bool trackDir; 

    private bool lockMoving = false;

    private bool leftShiled = true;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<UserInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        //camcom = GetComponent<camaraContoller>();
    }

    // Update is called once per frame
    void Update() // 1 / 60
    {
        if(Knapsack.Instance.CanvasGroups.alpha != 1)
        {
            AnimationUpdate();
            thrustVect = Vector3.zero;
        }
         

        if(lockMoving == false && Knapsack.Instance.CanvasGroups.alpha != 1)
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
            anim.SetFloat("right", 0);
        
//        else
 //       {
 //           Vector3 localVect = transform.InverseTransformDirection(pi.Dvec);
//            anim.SetFloat("forward", localVect.z * targetRunMult);
 //           anim.SetFloat("right", localVect.x * targetRunMult);
 //       }
        
        

        if (camcom.lockState == false)
        {

        }

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
        if(pi.roll ||rigid.velocity.magnitude > 7f)
        {
            anim.SetTrigger("roll");
            canAttack = false;
            PlayerInfo.instance_.CosumeStamina();
        }

        //attack
        if ((pi.attack && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack))
        //if(pi.attack && )
        {
            anim.SetTrigger("attack");
        }

        if (!pi.run)
        {
            anim.SetBool("defense", pi.defense);
        }
         
        
        
        //anim.SetBool("defense", true);
        // print(CheckState("idle", "attack"));

        // lock target
        if (pi.lockon)
        {
            camcom.lockUnlockTarget();
        }

        /*if (camcom.lockState)
        {
            if (!trackDir)
                model.transform.forward = transform.forward;
            else
                model.transform.forward = movingVect.normalized;
        }
        */
        // defense
        
    }

    private void FixedUpdate()  // 1 / 50
    {
        if(Knapsack.Instance.CanvasGroups.alpha != 1)
        {
            rigid.position += deltaPos; // OnAnimatorUpdateRM y fixedUpdate no es de mismo tiempo
                                        //rigid.position += movingVect * Time.fixedDeltaTime;
            rigid.velocity = new Vector3(movingVect.x, rigid.velocity.y, movingVect.z) + thrustVect; // añadoir velocidad al personaje
            thrustVect = Vector3.zero; // for impulso
            deltaPos = Vector3.zero;
        }
        
    }
    //metodo para comprobar el estado de la animacion
    public bool CheckState(string stateName, string layerName = "Base Layer" )
    {
        int layerIndex_ = anim.GetLayerIndex(layerName);
        bool output = anim.GetCurrentAnimatorStateInfo(layerIndex_).IsName(stateName);
        return output;
    }
    //Metodos para comprobar el tag de la animacion
    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex_ = anim.GetLayerIndex(layerName);
        bool output = anim.GetCurrentAnimatorStateInfo(layerIndex_).IsTag(tagName);
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
        trackDir = true;
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
        trackDir = true;
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
        trackDir = true;
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
 

    public void OnAttack1AUpdate()
    {
        thrustVect = model.transform.forward * anim.GetFloat("attackVelocity"); // ataque avanza
        /*float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack")); // cambio de weight con interpolacion
		currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
		anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);*/
        

    }

    public void OnAttackIdleUpdate()
    {
        /*float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack")); // cambio de peso con interpolacion
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        //print(currentWeight)
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), lerpTarget);*/

    }

    public void OnAttackExit()
    {
        anim.gameObject.SendMessage("WeaponDisable");
    }

    //
    public void OnAnimatorUpdateRM(Vector3 animDeltaPos)
    {
        //print(deltaPos);
        if((CheckState("attack1hC") || CheckState("roll")) && Knapsack.Instance.CanvasGroups.alpha != 1)
        {
            thrustVect = Vector3.zero;
            deltaPos += animDeltaPos;           
        }
        
    }

    public void OnHitEnter()
    {
        pi.inputEnable = false;
        //movingVect = Vector3.zero;
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
}
