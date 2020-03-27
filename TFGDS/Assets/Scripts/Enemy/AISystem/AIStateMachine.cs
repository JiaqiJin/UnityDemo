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

public enum AITriggerEventType
{
    Enter, Stay, Exit
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
    protected AIState currentState_ = null;
    protected Dictionary<AIStateType, AIState> state_ = new Dictionary<AIStateType, AIState>();
    protected AITarget target_ = new AITarget();

    [SerializeField]
    protected SphereCollider targetTrigger_ = null;
    [SerializeField]
    protected SphereCollider sensorTrigger_ = null;
    [SerializeField]
    protected AIStateType currentStateType_ = AIStateType.Idle;

    //distance de stop al target objeto
    [SerializeField]
    [Range(0, 15)] protected float stoppingDistance_ = 1.0f;

    //Componentes
    protected Animator animator_ = null;
    protected NavMeshAgent navAgent_ = null;
    protected Collider collider_ = null;
    protected Transform transform_ = null;
    //getter 
    public Animator animator
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

    public Vector3 sensorPosition
    {
        get
        {
            if (sensorTrigger_ == null) return Vector3.zero;
            //calcular la escala nuevo ya que el transform principal cuando cambia no afecta al sensor transform
            Vector3 point = sensorTrigger_.transform.position;
            point.x += sensorTrigger_.center.x * sensorTrigger_.transform.lossyScale.x;
            point.y += sensorTrigger_.center.y * sensorTrigger_.transform.lossyScale.x;
            point.z += sensorTrigger_.center.z * sensorTrigger_.transform.lossyScale.x;
            return point;
        }
    }

    public float SensorRadius
    {
        get
        {
            if (sensorTrigger_ == null) return 0.0f;
            // el maximo radio del sensor cuando cambia el radio del padre
            float radius = Mathf.Max(sensorTrigger_.radius * sensorTrigger_.transform.localScale.x,
                                     sensorTrigger_.radius * sensorTrigger_.transform.localScale.y);

            return Mathf.Max(radius, sensorTrigger_.radius * sensorTrigger_.transform.localScale.z);
        }
    }

    protected virtual void Start()
    {
        if(sensorTrigger_ != null)
        {
            AISensor script = sensorTrigger_.GetComponent<AISensor>();
            if(script != null)
            {
                script.parentStateMachine = this;
            }
        }

        AIState[] states = GetComponents<AIState>();

        foreach (AIState state in states)
        {
            if (state != null && !state_.ContainsKey(state.GetStateType()))
            {
                state_[state.GetStateType()] = state;
                state.SetStateMachine(this);
            }
        }

        if (state_.ContainsKey(currentStateType_))
        {
            currentState_ = state_[currentStateType_];
            currentState_.OnEnterState();
        }
        else
        {
            currentState_ = null;
        }
    }
    /// <summary>
    /// Mostrar el estado actual y posibles cambios de update y cambiar transiciones
    /// </summary>
    protected virtual void Update()
    {
        if (currentState_ == null)
        {
            return;
        }

        AIStateType newStateType = currentState_.OnUpdate();
        if (newStateType != currentStateType_)
        {
            AIState newState = null;
            if (state_.TryGetValue(newStateType, out newState))
            {
                currentState_.OnExitState();
                newState.OnEnterState();
                currentState_ = newState;
            }
            else
            {
                if (state_.TryGetValue(AIStateType.Idle, out newState))
                {
                    currentState_.OnExitState();
                    newState.OnEnterState();
                    currentState_ = newState;
                }
            }
            currentStateType_ = newStateType;

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

    protected virtual void Awake()
    {
        transform_ = transform;
        animator_ = GetComponent<Animator>();
        navAgent_ = GetComponent<NavMeshAgent>();
        collider_ = GetComponent<Collider>();

        if (GameSceneManager.instance != null)
        {
            //registrar state maquina en el diccionario del game manager
            if (collider_) GameSceneManager.instance.RegisterAIStateMachine(collider_.GetInstanceID(), this);
            if (sensorTrigger_) GameSceneManager.instance.RegisterAIStateMachine(sensorTrigger_.GetInstanceID(), this);
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

    public void ClearTarget()
    {
        target_.Clear();
        if (targetTrigger_ != null)
        {
            targetTrigger_.enabled = false;
        }
    }
    /// <summary>
    /// llama al sistema physica de unity y llegar al player 
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (targetTrigger_ == null || other != targetTrigger_) return;

        if (currentState_)
            currentState_.OnDestinationReached(true);
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider other)
    {
        if (targetTrigger_ == null || other != targetTrigger_) return;

        if (currentState_ != null)
            currentState_.OnDestinationReached(false);
    }


    public virtual void OnTriggerEvent(AITriggerEventType type , Collider other)
    {
        if (currentState_ != null)
            currentState_.OnTriggerEvent(type, other);
    }

  
    protected virtual void OnAnimatorMove()
    {
        if(currentState_ != null)
        {
            currentState_.OnAnimatorUpdate();
        }
    }

    protected virtual void OnAnimatorIK(int layerIndex)
    {
        if (currentState_ != null)
            currentState_.OnAnimatorIKUpdate();
    }

    public void NavAgentControl(bool positionUpdate, bool rotationUpdate)
    {
        if (navAgent_)
        {
            navAgent_.updatePosition = positionUpdate;
            navAgent_.updateRotation = rotationUpdate;
        }
    }
     
}
