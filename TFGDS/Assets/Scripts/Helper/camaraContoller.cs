using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraContoller : MonoBehaviour
{
    public UserInput pi; // entrada del player
    public float horizontalSpeed = 20.0f; // velocidad para eje horizontal
    public float verticalSpeed = 80.0f; // velocidada para el eje vertical;
    public float camaraDappValue = 0.5f;

    private GameObject player; // objeto player
    private GameObject camaraHandler; // objeto camara
    private float tempEulex; // variable temporar para la el angulo euler de la camara;
    private GameObject model;
    private Camera camara; // variable para alamacenar la camara principal


    private Vector3 camaraDampVelocity;
    // Start is called before the first frame update
    void Start()
    {
        camaraHandler = transform.parent.gameObject;
        player = camaraHandler.transform.parent.gameObject;
        pi = player.GetComponent<UserInput>();
        tempEulex = 20;
        model = player.GetComponent<AnimatoContoller>().model;
        camara = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        player.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime); // se mueve el objeto`player por el axi horizontal
        //camaraHandler.transform.Rotate(Vector3.right, pi.Jup * verticalSpeed * Time.deltaTime); // se mueve la camara  por axis vertical
        tempEulex -= pi.Jup * verticalSpeed * Time.fixedDeltaTime; // se resta ya que se mueve del sentido contratrio;
        tempEulex = Mathf.Clamp(tempEulex, -40, 30); // maximo angulo de rotacion
        camaraHandler.transform.localEulerAngles = new Vector3(tempEulex,0,0);
        // asiganr el euler angulo de ese momento del update
        model.transform.eulerAngles = tempModelEuler;

        //asigno las posicion del camaraPos al posiicon de la camara main
        camara.transform.position = Vector3.SmoothDamp(camara.transform.position, transform.position, ref camaraDampVelocity, camaraDappValue);
        //camara.transform.eulerAngles = transform.eulerAngles;
        camara.transform.LookAt(camaraHandler.transform);
    }
}
