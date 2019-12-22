using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camaraContoller : MonoBehaviour
{
    public UserInput pi; // entrada del player
    public float horizontalSpeed = 20.0f; // velocidad para eje horizontal
    public float verticalSpeed = 80.0f; // velocidada para el eje vertical;
    public float camaraDappValue = 0.5f;
    public Image lockIcon;
    public bool lockState;

    private GameObject player; // objeto player
    private GameObject camaraHandler; // objeto camara
    private float tempEulex; // variable temporar para la el angulo euler de la camara;
    private GameObject model;
    private Camera camara; // variable para alamacenar la camara principal

     
    private Vector3 camaraDampVelocity;
    [SerializeField]
    private GameObject lockTarget;
    // Start is called before the first frame update
    void Start()
    {
        camaraHandler = transform.parent.gameObject;
        player = camaraHandler.transform.parent.gameObject;
        AnimatoContoller ac = player.GetComponent<AnimatoContoller>();
        pi = ac.pi;
        tempEulex = 20;
        model = ac.model;
        camara = Camera.main;
        lockIcon.enabled = false;
        lockState = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // si no lock se gira libremente
        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            player.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime); // se mueve el objeto`player por el axi horizontal
            //camaraHandler.transform.Rotate(Vector3.right, pi.Jup * verticalSpeed * Time.deltaTime); // se mueve la camara  por axis vertical
            tempEulex -= pi.Jup * verticalSpeed * Time.fixedDeltaTime; // se resta ya que se mueve del sentido contratrio;
            tempEulex = Mathf.Clamp(tempEulex, -40, 30); // maximo angulo de rotacion
            camaraHandler.transform.localEulerAngles = new Vector3(tempEulex, 0, 0);
            // asiganr el euler angulo de ese momento del update
            model.transform.eulerAngles = tempModelEuler;
        }
        // gira la pa pos del objeto
        else
        {
            Vector3 tempForward = lockTarget.transform.position - model.transform.position;
            tempForward.y = 0;
            player.transform.forward = tempForward;
        }

       

        //asigno las posicion del camaraPos al posiicon de la camara main
        camara.transform.position = Vector3.SmoothDamp(camara.transform.position, transform.position, ref camaraDampVelocity, camaraDappValue);
        //camara.transform.eulerAngles = transform.eulerAngles;
        camara.transform.LookAt(camaraHandler.transform);
    }

    public void lockUnlockTarget()
    {
        //print("asd");
        //if(lockTarget == null)
        //{
            // lock
            Vector3 modelOrigin1 = model.transform.position;
            //print(modelOrigin1);
            Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
            Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));

        if(cols.Length == 0)
        {
            lockTarget = null;
            lockIcon.enabled = false;
            lockState = false;
        }

        else {
            foreach (var col in cols)
            { 
                if(lockTarget == col.gameObject)
                {
                    lockTarget = null;
                    lockIcon.enabled = false;
                    lockState = false;

                    break;
                }
                //print(col.name);
                lockTarget = col.gameObject;
                lockIcon.enabled = true;
                lockState = true;
                //lockIcon.transform.position = Camera.main.WorldToScreenPoint(lockTarget.transform.position);
                break;
            }
        }
            
        }
       
    //}

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
         Gizmos.DrawLine(transform.position, transform.localScale);
    }*/
}
