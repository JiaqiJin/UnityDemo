using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    private AnimatoContoller ac;
    //public AnimatorManager am;
    private Collider weaponColL;
    [SerializeField]
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

    // Start is called before the first frame update
    void Start()
    {
        //weaponCol = whR.GetComponentInChildren<Collider>();
        //print(transform.DeepFind("weaponHandleR")); 
        whL = transform.DeepFind("weaponHandleL").gameObject;
        whR = transform.DeepFind("weaponHandleR").gameObject;
        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();

        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempwc;
        tempwc = targetObj.GetComponent<WeaponController>();
        if(tempwc == null)
        {
            tempwc = targetObj.AddComponent<WeaponController>();
        }
        tempwc.wm = this;
        return tempwc;
    }

    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        //weaponColL.enabled = false;
    }
    public void WeaponEnable()
    {
        weaponColR.enabled = true;
    }
}
