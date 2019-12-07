using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatoContoller : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 1.4f;
    public float runMultiplier;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVect;
    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() // 1 / 60
    {
        anim.SetFloat("forward", pi.Dmag * ((pi.run) ? 2.0f : 1.0f));
        if (pi.Dmag > 0.1f) {
            // giro del personajes
            model.transform.forward = pi.Dvec;
        }
        // vector  que alamacena la veclocidad 
        movingVect = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run)? runMultiplier : 1.0f);
    }

    private void FixedUpdate()  // 1 / 50
    {
        //rigid.position += movingVect * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(movingVect.x,rigid.velocity.y,movingVect.z); // añadoir velocidad al personaje
    }
}
