using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que hace funcion mas abstracto para añadir diferentes componentes a los manager del player
/// </summary>
public class IActorManagerInterface : MonoBehaviour
{
    public AnimatorManager am;

  
    /// <summary>
    /// TODO  conectar con el inventario y usar objeto de alli para recuperar el hp de player
    /// </summary>
    /// 
    public void AddHP(int value)
    {
        PlayerInfo.instance_.HealthAdd(value);
    }


}
