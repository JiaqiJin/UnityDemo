using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{
    //public AnimatorManager am; 

    private CapsuleCollider defCol;
    // Start is called before the first frame update
    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = new Vector3(0,1,0);
        defCol.height = 2.0f;
        defCol.radius = 0.25f;
        defCol.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        WeaponController targetWc = col.GetComponent<WeaponController>();
        //print(col.name);
        
        /*GameObject attacker = targetWc.wm.am.gameObject;
        GameObject receiver = am.gameObject;
        // solo se pude atacar entre cierto angulos
        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        //Vector3 counterDir = attacker.transform.position - receiver.transform.position;
        float attackAngle1 = Vector3.Angle(attacker.transform.forward, attackingDir);

        bool attackValid = (attackAngle1 < 45);
        */
        if (col.tag == "weapon")
        {
            //if(attackValid)
           // {
                am.TryDoDamage();  
            //}
            
        }
    }
}
