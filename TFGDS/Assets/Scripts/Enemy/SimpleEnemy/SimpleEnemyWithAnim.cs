using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyWithAnim : MonoBehaviour
{
    private GameObject playerUnit;
    private Animator anim;
    private Vector3 initialPosition;

    public GameObject model;

    public float wanderRadius;          //radio del enemigo 
    public float alertRadius;         //El radio de alerta cuando el jugador entra el monstruo advertirá y enfrentará al jugador todo el tiempo.
    public float defendRadius;          //Radio de autodefensa Después de que el jugador entre, el monstruo lo perseguirá.
    public float chaseRadius;            //Radio de persecución cuando el monstruo excede el radio de persecución abandonará la persecución 

    public float attackRange;            //rango de ataque
    public float walkSpeed;          //velocidad 
    public float runSpeed;          //velocidad de correr
    public float turnSpeed;         //giro

    private enum MonsterState
    {

        STAND,
        CHECK,
        WALK,
        WARN,
        Attack,
        CHASE,// perseguir jugadores
        RETURN
    }


    private MonsterState currentState = MonsterState.STAND;

    //establecer el peso de varias acciones en espera, observar, moverse...
    public float[] actionWeight = { 3000, 3000, 4000 };
    public float actRestTime;
    public float lastActTime;

    private float distanceToPlayer;
    private float distanceToInitial;
    private Quaternion targetRotation;


    private bool is_warned = false;
    private bool is_Running = false;
    private bool is_Attack = false;


    // Start is called before the first frame update
    void Start()
    {
        playerUnit = GameObject.Find("Player");
        //this.anim = GetComponentInChildren<Animator>();
        anim = model.GetComponent<Animator>();

        //pos inicial
        initialPosition = gameObject.GetComponent<Transform>().position;

        defendRadius = Mathf.Min(alertRadius, defendRadius);

        attackRange = Mathf.Min(defendRadius, attackRange);

        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);

        RandomAction();
    }

 
    void RandomAction()
    {
        lastActTime = Time.time;

        /* float number = Random.Range(0, actionWeight[0] + actionWeight[1] + actionWeight[2]);

         if(number <= actionWeight[0])
         {
             currentState = MonsterState.STAND;
             this.anim.SetTrigger("Stand");
         }
         else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1]) 
         {
             currentState = MonsterState.STAND;
             this.anim.SetTrigger("Stand");
         }
         if(actionWeight[0] + actionWeight[1] < number && number <= actionWeight[0] + actionWeight[1] + actionWeight[2])
         {
             currentState = MonsterState.WALK;
             //random direction faced
             targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
             this.anim.SetTrigger("Walk");
         }*/

        currentState = MonsterState.STAND;
        this.anim.SetTrigger("Stand");

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case MonsterState.STAND:
                if(Time.time - lastActTime > actRestTime)
                {
                    RandomAction();
                }
                EnemyDistanceCheck();
            break;

            case MonsterState.CHECK:
                if (Time.time - lastActTime > anim.GetCurrentAnimatorStateInfo(0).length)
                {
                    RandomAction();
                }
                EnemyDistanceCheck();
                break;

            case MonsterState.WALK:
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);

                if (Time.time - lastActTime > actRestTime)
                {
                    RandomAction();        
                }
         
                WanderRadiusCheck();
                break;

            case MonsterState.WARN:

                if (!is_warned)
                {
                    this.anim.SetTrigger("Warn");
                    //plñay audio
                    is_warned = true;
                }
                //rota a la posiscion del jugador
                targetRotation = Quaternion.LookRotation(playerUnit.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);

                WarningCheck();
                break;

            case MonsterState.CHASE:
                if (!is_Running)
                {
                    anim.SetTrigger("Run");
                    is_Running = true;
                }
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
                //rota a la posiscion del jugador
                targetRotation = Quaternion.LookRotation(playerUnit.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                ChaseRadiuCheck();
                break;

            case MonsterState.RETURN:

                targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);

                ReturnCheck();
                break;

            case MonsterState.Attack:
                if (!is_Attack)
                {
                    anim.SetTrigger("Attack");
                    is_Attack = true;
                }

                AttckCHeck();

                break;
        }
    }
    //comprobar el estado 
    void EnemyDistanceCheck()
    {
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        if(distanceToPlayer < attackRange)
        {
            currentState = MonsterState.Attack;
        }
        else if(distanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if(distanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }
    }

    void WarningCheck()
    {
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);

        if(distanceToPlayer < defendRadius)
        {
            is_warned = false;
            currentState = MonsterState.CHASE;
        }
        if(distanceToPlayer > alertRadius)
        {
            is_warned = true;
            RandomAction();
        }

    }

    void WanderRadiusCheck()
    {
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (distanceToPlayer < attackRange)
        {
            currentState = MonsterState.Attack;
        }
        else if (distanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if (distanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }

        if (distanceToPlayer > wanderRadius)
        {
            // giro del enemy
            targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
        }
    }

    void ChaseRadiuCheck()
    {
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (distanceToPlayer < attackRange)
        {

            currentState = MonsterState.Attack;
        }
        //si slae de la area del enemigo retorna a ka pos inicial
        if(distanceToInitial > chaseRadius || distanceToPlayer > alertRadius)
        {
            currentState = MonsterState.RETURN;
        }

    }

    void ReturnCheck()
    {
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);
        //si esta cerca , random action
        if (distanceToInitial < 0.5f)
        {
            is_Running = false;
            RandomAction();
        }
    }

    void AttckCHeck()
    {
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (distanceToPlayer < attackRange)
        {
            currentState = MonsterState.Attack;
            is_Attack = true;
        }
        else if (distanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if (distanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }

    }

}
