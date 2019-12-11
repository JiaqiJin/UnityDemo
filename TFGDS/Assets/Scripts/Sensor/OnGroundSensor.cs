using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    //Returns an array with all colliders touching or inside the sphere.
    public CapsuleCollider capCol;

    private Vector3 point1; // punto de pos para la parte inferior 
    private Vector3 point2; // punto de posicion para la parte superior
    private float radius;
    public float offset = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        radius = capCol.radius - 0.05f;     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius - offset);
        point2 = transform.position + transform.up * (capCol.height - offset) - transform.up * radius;

        Collider[] collision = Physics.OverlapCapsule(point1, point2, radius , LayerMask.GetMask("Ground")); 
        if (collision.Length != 0) 
        {
            //print("collision");
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }*/
}
