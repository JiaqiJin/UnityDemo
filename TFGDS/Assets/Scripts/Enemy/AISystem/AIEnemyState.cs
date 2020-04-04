using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public abstract class AIEnemyState : AIState
{

    protected int playerLaterMask_ = -1;
    protected int bodyPartLayer_ = -1;
    private void Awake()
    {
        playerLaterMask_ = LayerMask.GetMask("Player", "AI Body Part") + 1;
        bodyPartLayer_ = LayerMask.GetMask("AI Body Part") ;
    }

    public override void OnTriggerEvent(AITriggerEventType eventType, Collider other)
    {

        if (statemachine_ == null)
            return;

        if(eventType != AITriggerEventType.Exit)
        {
            AITargetType curType = statemachine_.VisualThreat.type;

            if(other.CompareTag("Player"))
            {
                float distance = Vector3.Distance(statemachine_.sensorPosition, other.transform.position);//distancia de amenaza
                //comproueba el amenaza visual si es player
                if (curType != AITargetType.Visual_Player
                    ||(curType == AITargetType.Visual_Player && distance < statemachine_.VisualThreat.distance))
                {
                    RaycastHit hitInfo;
                    if(colliderIsVisible(other, out hitInfo, playerLaterMask_))
                    {
                        statemachine_.VisualThreat.Set(AITargetType.Visual_Player, other, other.transform.position, distance);
                    }
                }
            }


        }

    }
    /// <summary>
    /// Si se puede detectar la presencia del jugador (Aislamiento de obstáculos)
    /// </summary>
    /// <param name="other"></param>
    /// <param name="hitInfo"></param>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    protected virtual bool colliderIsVisible(Collider other, out RaycastHit hitInfo, int layerMask = -1)
    {
        hitInfo = new RaycastHit();

        if (statemachine_ == null)
            return false;

        AIEnemyStateMachine enemyMachine = (AIEnemyStateMachine)statemachine_;
        //1.limite el campo de visión
        //Determine si está en el campo de visión FOV.
        Vector3 head = statemachine_.sensorPosition;
        Vector3 direction = other.transform.position - head;
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle > (enemyMachine.fov * 0.5))
            return false;


        RaycastHit[] hits = Physics.RaycastAll(head, direction.normalized, statemachine_.SensorRadius * enemyMachine.sight, layerMask);

        //encontrar el collider mas cercano que no pertenece al ai body part
        float closestColliderDistance = float.MaxValue;
        Collider closestCollider = null;

        for(int i =0;i<hits.Length;i++)
        {
            RaycastHit hit = hits[i];
            if(hit.distance < closestColliderDistance)
            {
                if(hit.transform.gameObject.layer == bodyPartLayer_) //ai_body layer
                {
                    if (statemachine_ != GameSceneManager.instance.GetAIStateMachine(hit.rigidbody.GetInstanceID())) // not mismo layer
                    {
                        closestColliderDistance = hit.distance;
                        closestCollider = hit.collider;
                        hitInfo = hit;
                    }
                }
                else
                {
                    closestColliderDistance = hit.distance;
                    closestCollider = hit.collider;
                    hitInfo = hit;
                }
            }

        }
        //hay un jugador en el campo de visión.
        if (closestCollider.gameObject == other.gameObject && closestCollider)
        {
            return true;
        }

        return false;
    }

}



