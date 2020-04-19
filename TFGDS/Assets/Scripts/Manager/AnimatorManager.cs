using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public AnimatoContoller ac;
    public BattleManager bm;
    public WeaponManager wm;
    public PlayerSatetManage sm;

    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<AnimatoContoller>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;

        bm = Bind<BattleManager>(sensor);

        wm = Bind<WeaponManager>(model);

        sm = Bind<PlayerSatetManage>(gameObject);
        //sm.Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Metodos que vincula a los diferentes manager algunas funciones que comparte
    /// </summary>
    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if(tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }

    public void TryDoDamage()
    {
        if(PlayerInfo.instance_.HP > 0)
        {
            PlayerInfo.instance_.HealthDamageRest(40);
            Hit();
        }     
        else
        {
            Die();
        }
        //print("do damage");
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }


    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnable = false;
        if(ac.camcom.lockState == true)
        {
            ac.camcom.lockUnlockTarget();
            
        }
        ac.camcom.enabled = false;
    }
}
