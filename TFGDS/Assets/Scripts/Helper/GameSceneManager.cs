using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    //static 
    private static GameSceneManager instance_ = null;
    public static GameSceneManager instance
    {
        get
        {
            if (instance_ == null)
                instance_ = (GameSceneManager)FindObjectOfType(typeof(GameSceneManager));
            return instance_;
        }
    }

    //private mienbros
    private Dictionary<int, AIStateMachine> stateMachines_ = new Dictionary<int, AIStateMachine>();

    //public methods
    /// <summary>
    /// Guarda el estado de maquina que le pasas
    /// </summary>
    /// <param name="key"></param>
    /// <param name="stateMachine"></param>
    public void RegisterAIStateMachine(int key , AIStateMachine stateMachine)
    {
        if (!stateMachines_.ContainsKey(key))
        {
            stateMachines_[key] = stateMachine;
        }

    }
    /// <summary>
    /// Retorna el estado del maquina pasando un iD(key)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public AIStateMachine GetAIStateMachine(int key)
    {
        AIStateMachine machine = null;

        if(stateMachines_.TryGetValue(key , out machine))
        {
            return machine;
        }

        return null;
    }


}
