using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("======== Key Setting ========")]
    //Variables para entrada de teclados
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyA; // almace run 
    public string keyB;
    public string keyC;
    public string keyD;
    //key <- , -> ..
    public string keyJup;
    public string keyJdown;
    public string keyJleft;
    public string keyJright;
    

    [Header("======== Output Señal ========")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec; // vector 3 para el giro del personaje segun la entrada
    public float Jup;
    public float Jright;


    public bool run;
    public bool jump;
    private bool lastJump;

    [Header("======== Otros ========")]
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
        Jup = (Input.GetKey(keyJup) ? 1.0f : 0) - (Input.GetKey(keyJright) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJright) ? 1.0f : 0) - (Input.GetKey(keyJleft) ? 1.0f : 0);
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

        Vector2 tempVect = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempVect.x;
        float Dup2 = tempVect.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2)); // movimiento del personajes
        Dvec = Dright * transform.right + Dup * transform.forward; // giro del personakes

        run = Input.GetKey(keyA);
        Jump();
    }

    //http://squircular.blogspot.com/2015/09/mapping-circle-to-square.html
    private Vector2 SquareToCircle(Vector2 inputArea)
    {
        Vector2 output = Vector2.zero;

        output.x = inputArea.x * Mathf.Sqrt(1 - (inputArea.y * inputArea.y) / 2.0f);
        output.y = inputArea.y * Mathf.Sqrt(1 - (inputArea.x * inputArea.x) / 2.0f);
        return output;
    }

    void Jump()
    {
        bool tempJump = Input.GetKey(keyB);
        if (tempJump != lastJump && tempJump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = tempJump;

    }
}
