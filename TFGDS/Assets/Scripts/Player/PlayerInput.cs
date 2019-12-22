using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://gameprogrammingpatterns.com/contents.html
public class PlayerInput : UserInput
{
    [Header("======== Key Setting ========")]
    //Variables para entrada de teclados
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;

    public string keyA; // almace run 
    public string keyB; // jump
    public string keyC; // attack
    public string keyD; // defense
    public string keyE; // key lock
    //key <- , -> ..
    public string keyJup;
    public string keyJdown;
    public string keyJleft;
    public string keyJright;

    [Header("======== Mouse Setting ========")]
    public float mouseSensX = 1.0f;
    public float mouseSensY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        //Jup = (Input.GetKey(keyJup) ? 1.0f : 0) - (Input.GetKey(keyJdown) ? 1.0f : 0);
        // Jright = (Input.GetKey(keyJright) ? 1.0f : 0) - (Input.GetKey(keyJleft) ? 1.0f : 0);
        Jup = Input.GetAxis("Mouse Y")  * mouseSensX;
        Jright = Input.GetAxis("Mouse X")  * mouseSensX;
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
        defense = Input.GetKey(keyD);
        lockon = Input.GetKey(keyE);
        Jump();
        Attack();
    }
   
    //jump input
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
    //attack input 
    void Attack()
    {
        bool tempAttack = Input.GetKey(keyC);
        //print(tempAttack);
        if (tempAttack != lastAtttack && tempAttack == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAtttack = tempAttack;

    }

   
}
