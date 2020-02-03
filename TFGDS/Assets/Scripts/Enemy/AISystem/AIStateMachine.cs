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

    

}
