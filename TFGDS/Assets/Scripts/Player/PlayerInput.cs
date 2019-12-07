using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Variables para entrada de teclados
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;


    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;


    public bool inputEnable = true; // varibale para comprobar si puede recibir señal o no

    // variables para smoothdamp
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
       
        //Entrada de la coordenado x and y 
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (!inputEnable)
        {
            targetDup = 0;
            targetDright = 0;
        }

        //Gradually changes a value towards a desired goal over time.
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        Dmag = Mathf.Sqrt((Dup * Dup) + (Dright * Dright)); // movimiento del personajes
        Dvec = Dright * transform.right + Dup * transform.forward; // giro del personakes
    }
}
