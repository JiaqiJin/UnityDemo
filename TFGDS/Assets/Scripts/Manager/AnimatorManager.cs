using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public AnimatoContoller ac;
    public BattleManager bm;
    public WeaponManager wm;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<AnimatoContoller>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("Sensor").gameObject;
        bm = sensor.GetComponent<BattleManager>();
        if(bm == null)
        {
            bm = sensor.AddComponent<BattleManager>();
        }
        bm.am = this;

        wm = model.GetComponent<WeaponManager>();
        if(wm == null)
        {
            wm = model.AddComponent<WeaponManager>();
        }

        wm.am = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage()
    {
        ac.IssueTrigger("hit");
        //print("do damage");
    }


}
