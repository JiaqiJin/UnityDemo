using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour
{
    public AnimatorManager am; 

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
        //print(col.name);
        if(col.tag == "weapon")
        {
            am.DoDamage();
        }
    }
}
