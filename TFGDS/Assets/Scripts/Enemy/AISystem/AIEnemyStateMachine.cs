using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyStateMachine : AIStateMachine
{

    [SerializeField] [Range(10.0f,360.0f)] float fvo_ = 50.0f; //field view
    [SerializeField] [Range(0.0f,1.0f)] float sight_ = 0.5f; //vision
    [SerializeField] [Range(0.0f,1.0f)] float hearing_ = 1.0f; //
    [SerializeField] [Range(0.0f,1.0f)] float aggression_ = 0.5f; //field view
    [SerializeField] [Range(0,100)] int health_ = 100; //field view

    [SerializeField] [Range(0.0f, 1.0f)] float intelligence_ = 0.5f; //field view
    [SerializeField] [Range(0.0f, 1.0f)] float satisfaction_ = 1.0f; //field view

    private int seeking_ = 0;
    private int attackTpe_ = 0;
    //private float speed_;


    // Hashes
    private int speedHash_ = Animator.StringToHash("speed");
    private int seekingHash_ = Animator.StringToHash("seeking");
    private int attackHash_ = Animator.StringToHash("attack");

    // Public Properties
    public float fov { get { return fvo_; } }
    public float hearing { get { return hearing_; } }
    public float sight { get { return sight_; } }
    public float intelligence { get { return intelligence_; } }
    public float satisfaction { get { return satisfaction_; } set { satisfaction_ = value; } }
    public float aggression { get { return aggression_; } set { aggression_ = value; } }
    public int health { get { return health_; } set { health_ = value; } }
    public int attackType { get { return attackTpe_; } set { attackTpe_ = value; } }
    public int seeking { get { return seeking_; } set { seeking_ = value; } }
    public float speed
    {
        get { return navAgent_ != null ? navAgent_.speed : 0.0f; }
        set
        {
            if (navAgent_ != null) navAgent_.speed = value; 
        }
    }

    protected override void Update()
    {
        base.Update();

        if (animator_ != null)
        {
            animator_.SetFloat(speedHash_, navAgent_.speed);
            animator_.SetInteger(seekingHash_, seeking_);
            animator_.SetInteger(attackHash_, attackTpe_);
        }

    }

}
