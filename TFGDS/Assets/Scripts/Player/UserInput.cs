using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput : MonoBehaviour
{
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec; // vector 3 para el giro del personaje segun la entrada
    public float Jup;
    public float Jright;


    public bool run;
    public bool jump;
    protected bool lastJump;
    public bool attack;
    protected bool lastAtttack;


    [Header("======== Otros ========")]
    public bool inputEnable = true; // varibale para comprobar si puede recibir señal o no


    // variables para smoothdamp
    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    // metodos convierte rectangulo en circulo
    //http://squircular.blogspot.com/2015/09/mapping-circle-to-square.html
    protected Vector2 SquareToCircle(Vector2 inputArea)
    {
        Vector2 output = Vector2.zero;

        output.x = inputArea.x * Mathf.Sqrt(1 - (inputArea.y * inputArea.y) / 2.0f);
        output.y = inputArea.y * Mathf.Sqrt(1 - (inputArea.x * inputArea.x) / 2.0f);
        return output;
    }
}
