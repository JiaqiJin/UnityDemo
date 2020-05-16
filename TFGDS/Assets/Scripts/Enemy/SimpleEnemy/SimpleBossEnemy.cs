using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleBossEnemy : MonoBehaviour
{

    public float speed;
    //public float minDis, maxDis;
    //public float range;

    public int searchRange = 10;

    public int attackNumber;

    public PlayerSatetManage playerManager;

    private Animator animator;

    private EnemyState attribute;
    private Transform target;

    private bool isFoundPlayer;
    private bool isAttacking;
    private float attackTimer;

    public float AttackInterval = 3f;

    private NavMeshAgent navAgent = null;
    private GameObject[] mobPoints;
    public bool isPatrol = true;
    private int mobPointIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attackTimer = AttackInterval;
        target = GameObject.Find("Player").transform;
        attribute = GetComponent<EnemyState>();
        navAgent = GetComponent<NavMeshAgent>();
        if(isPatrol)
        {
            mobPoints = GameObject.FindGameObjectsWithTag("MobPoint");
        }
        if (mobPoints != null && mobPoints.Length > 0)
        {
            mobPointIndex = 0;
            navAgent.SetDestination(mobPoints[mobPointIndex].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attribute.IsDeath)
        {
            Death();
            return;
        }

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        isAttacking = false;
        for (int i = 1; i <= attackNumber; i++)
        {
            //if( state.IsName("Attack" + i) ) {
            if (animator.GetBool("attack" + i) == true)
            {
                isAttacking = true;
                break;
            }
        }

        //Debug.Log(isAttacking);
        if (isAttacking)
        {
            animator.SetBool("run", false);
            return;
        }

        if (InRange(attribute.attackDistance) && attackTimer < 0)
        {       // attack state
            navAgent.isStopped = true;
            attackTimer = AttackInterval;
            animator.SetBool("run", false);
            animator.SetBool("attack" + Random.Range(1, attackNumber + 1).ToString(), true);
        }
        else if (InRange(searchRange))
        {                                    // Found
            navAgent.isStopped = false;
            navAgent.SetDestination(target.gameObject.transform.position);
            animator.SetBool("run", true);
        }
        else if (navAgent.remainingDistance < 1 && mobPoints != null && mobPoints.Length > 0)
        {       // arrive at one patrol point
            navAgent.isStopped = false;
            mobPointIndex = (mobPointIndex + 1) % mobPoints.Length;
            navAgent.SetDestination(mobPoints[mobPointIndex].transform.position);
            animator.SetBool("run", true);
        }

    }

    bool InRange(float range)
    {
        var dis = Vector3.Distance(this.transform.position, target.transform.position);
        if (dis < range)
            return true;
        return false;
    }

    public void BeAttacked()
    {

        if (attribute.IsDeath)
            return;

        int damage = (int)Random.Range(100, 200);
        bool critical = damage > 150;

        animator.SetBool("hurt", true);

        attribute.TakeDamage(damage.ToString(), critical);

    }

    public void Attack()
    {


    }

    public void Death()
    {
        animator.SetBool("death", true);

        Destroy(this.gameObject, 3);
    }
}
