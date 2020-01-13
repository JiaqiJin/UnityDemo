using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase Timer para controlar el tiempo del jugador
/// </summary>
public class Timer 
{
    //enumerable para estbalecer el estado de la pulsacion del teclado
    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED
    }
    public STATE state;

    public float duration = 0f;

    private float elapsedTime = 0f;
    //metodos update de la clase para el tiempo
    public void Tick()
    {
        if(state == STATE.IDLE)
        {

        }
        else if (state == STATE.RUN) 
        {
            elapsedTime += Time.deltaTime; // añadir el tiempo 
            if(elapsedTime >= duration)
            {
                state = STATE.FINISHED;
            }

        }
        else if(state == STATE.FINISHED)
        {

        }
        else
        {
            Debug.Log("My timer Error !!!!");
        }

        //Debug.Log(Time.deltaTime);
    }
    //Funcion que compruba el estado del tiempo run = en estado de pulsacion
    public void GO()
    {
        elapsedTime = 0f;
        state = STATE.RUN;
    }
}
