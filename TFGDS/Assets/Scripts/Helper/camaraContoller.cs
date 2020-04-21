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
    private GameObject camara; // variable para alamacenar la camara principal
      
    private Vector3 camaraDampVelocity;
    [SerializeField]
    private LockTarget lockTarget;
    // Start is called before the first frame update
    void Start()
    {
        camaraHandler = transform.parent.gameObject;
        player = camaraHandler.transform.parent.gameObject;
        AnimatoContoller ac = player.GetComponent<AnimatoContoller>();
        pi = ac.pi;
        tempEulex = 20;
        model = ac.model;
        camara = Camera.main.gameObject;
        lockIcon.enabled = false;
        lockState = false;

        if (Knapsack.Instance.CanvasGroups.alpha == 1)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Knapsack.Instance.CanvasGroups.alpha == 1)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;

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
                Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
                tempForward.y = 0;
                player.transform.forward = tempForward;
                //camaraHandler.transform
                camaraHandler.transform.LookAt(lockTarget.obj.transform);
            }



            //asigno las posicion del camaraPos al posiicon de la camara main
            camara.transform.position = Vector3.SmoothDamp(camara.transform.position, transform.position, ref camaraDampVelocity, camaraDappValue);
            //camara.transform.eulerAngles = transform.eulerAngles;
            camara.transform.LookAt(camaraHandler.transform);
        }

       
    }

    void Update()
    {
        if(Knapsack.Instance.CanvasGroups.alpha != 1)
        {
            if (lockTarget != null)
            {
                //print
                lockIcon.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position);               
            }
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)
            {
                lockTarget = null;
                lockIcon.enabled = false;
                lockState = false;
            }
            AnimatorManager targetAm = lockTarget.obj.GetComponent<AnimatorManager>();
            if(targetAm != null && targetAm.sm.isDie) // si has muerto desvincula la mira
            {
                lockTarget = null;
                lockIcon.enabled = false;
                lockState = false;
            }

        }
       
       
    }

    public void lockUnlockTarget()
    {
        //print("asd");
        //
        //
            // lock
            Vector3 modelOrigin1 = model.transform.position;
            //print(modelOrigin1);
            Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
            Vector3 CenterLapbox = modelOrigin2 + model.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(CenterLapbox, new Vector3(0.5f, 0.5f, 5.0f), model.transform.rotation, LayerMask.GetMask("Enemy"));

        if(cols.Length == 0)
        {
            //print(cols.ToString());
            lockTarget = null;
            lockIcon.enabled = false;
            lockState = false;
        }

        else {
            foreach (var col in cols)
            {
                //print(col.name);
                if(lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    lockTarget = null;
                    lockIcon.enabled = false;
                    lockState = false;

                    break;
                }
                //print(col.name);
                lockTarget =new LockTarget( col.gameObject,col.bounds.extents.y);
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

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeigt;
        public AnimatorManager am;
        public LockTarget(GameObject obj_,float hal)
        {
            this.obj = obj_;
            this.halfHeigt = hal;
            am = obj_.GetComponent<AnimatorManager>();
        }

    }
}
