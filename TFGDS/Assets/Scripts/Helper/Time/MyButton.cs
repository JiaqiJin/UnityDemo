using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton 
{
    /// <summary>
    /// Varibales para asiganr a la entrada por el teclado detectando sus estado 
    /// </summary>
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExteding = false;
    public bool IsDelaying = false;
    /// <summary>
    /// Setting
    /// </summary>
    public float extendingDuration = 0.15f;
    public float delayingDuration = 0.15f;

    private bool currentState = false;
    private bool lastState = false;

    private Timer exitTime = new Timer();
    private Timer delayTime = new Timer();
    //metodos update de la clase para el button
    public void Tick(bool input)
    {
        //if (Input.GetKeyDown(KeyCode.P)) { 
        //}
        //Debug.Log(exitTime.state);

       
        //update time
        exitTime.Tick();
        delayTime.Tick();

        currentState = input;

        IsPressing = currentState;

        OnPressed = false; 
        OnReleased = false;
        IsExteding = false;
        IsDelaying = false;

        if(currentState != lastState) // compruebo el estado 
        {
            if(currentState == true) // si pulsa teclado
            {
                OnPressed = true;
                StartTimer(delayTime, delayingDuration); // tiempo de delay con su duracion
            }
            else // soltar el teclado 
            {
                OnReleased = true;
                StartTimer(exitTime, extendingDuration);// tiempo de exit con su duracion
            }
        }
        lastState = currentState;
        //Compruba el estado del tiempo 
        if(exitTime.state == Timer.STATE.RUN)
        {
            IsExteding = true;
        }
       
        
        if(delayTime.state == Timer.STATE.RUN)
        {
            IsDelaying = true;
        }

    }

    private void StartTimer(Timer timer,float duration)
    {
        timer.duration = duration;
        timer.GO();
    }

}
