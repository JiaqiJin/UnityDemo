using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//diferentes estados del AI 
public enum AIStateType
{
    None, Idle, Alerted, Patrol, Attack, Pursuit, Dead
}

public enum AITargetType
{
    None, WayPoint, Visual_Player
}

/// <summary>
/// Describe el objeto para el sistema del IA
/// </summary>
public struct AITarget
{
    private AITargetType type_; // tipo de objeto
    private Collider collider_; // collider
    private Vector3 position_; // current posicion en el mapa
    private float distance_; // distance al jugador
    private float time_; // tiempo que ha visto a un objeto(espera)

    public AITargetType type { get { return type_; } }
    public Collider collider { get { return collider_; } }
    public Vector3 position { get { return position_; } }
    public float distance { get { return distance_; } set { distance_ = value; } } 
    public float time { get { return time_; } }

    //Setter del AITraget
    public void Set(AITargetType t , Collider c, Vector3 p , float d)
    {
        type_ = t;
        collider_ = c;
        position_ = p;
        distance_ = d;
        time_ = Time.time;
    }

    public void Clear()
    {
        type_ = AITargetType.None;
        collider_ = null;
        position_ = Vector3.zero;
        distance_ = Mathf.Infinity;
        time_ = 0.0f;
    }
}



public abstract class AIStateMachine : MonoBehaviour
{

    //Publicos
    public AITarget VisualThreat = new AITarget();


    //protected Member
    protected Dictionary<AIStateType, AIState> state_ = new Dictionary<AIStateType, AIState>();
    protected AITarget target_ = new AITarget();

    [SerializeField]
    protected SphereCollider targetTrigger_ = null;
    [SerializeField]
    protected SphereCollider sensor_ = null;
    [SerializeField]
    protected AIStateType currentStateType = AIStateType.Idle;

    //distance de stop añ target objeto
    [SerializeField]
    [Range(0, 15)] protected float stoppingDistance_ = 1.0f;

    //Componentes
    protected Animator animator_ = null;
    protected NavMeshAgent navAgent_ = null;
    protected Collider collider_ = null;
    protected Transform transform_ = null;
    //getter 
    public Animator aninimator
    {
        get
        {
            return animator_;
        }
    }
    public NavMeshAgent navAgent
    {
        get
        {
            return navAgent_;
        }
    }

    protected virtual void Start()
    {
        AIState[] states = GetComponents<AIState>();

        foreach (AIState state in states)
        {
            if (state != null && !state_.ContainsKey(state.GetStateType()))
            {
                state_[state.GetStateType()] = state;
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        VisualThreat.Clear();
        if (target_.type != AITargetType.None)
        {
            target_.distance = Vector3.Distance(transform_.position, target_.position);
        }

    }

    // setter al current objeto target
    public void SetTarget(AITargetType t, Collider c, Vector3 p, float d)
    {
        target_.Set(t, c, p, d);

        if (targetTrigger_ != null)
        {
            targetTrigger_.radius = stoppingDistance_;
            targetTrigger_.transform.position = target_.position;
            targetTrigger_.enabled = true;
        }
    }
    // setter al current objeto target y le psasa el radio
    public void SetTarget(AITargetType t, Collider c, Vector3 p, float d, float s)
    {
        target_.Set(t, c, p, d);

        if (targetTrigger_ != null)
        {
            targetTrigger_.radius = s;
            targetTrigger_.transform.position = target_.position;
            targetTrigger_.enabled = true;
        }
    }

    public void SetTarget(AITarget t)
    {
        target_ = t;
        if (targetTrigger_ != null)
        {
            targetTrigger_.radius = stoppingDistance_;
            targetTrigger_.transform.position = target_.position;
            targetTrigger_.enabled = true;
        }
    }

    public void Clear()
    {
        target_.Clear();
        if (targetTrigger_ != null)
        {
            targetTrigger_.enabled = false;
        }
    }

}
