using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private AnimatoContoller ac;
    public AnimatorManager am;
    private Collider weaponColL;
    [SerializeField]
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    // Start is called before the first frame update
    void Start()
    {
        //weaponCol = whR.GetComponentInChildren<Collider>();
        //print(transform.DeepFind("weaponHandleR")); 
        whL = transform.DeepFind("weaponHandleL").gameObject;
        whR = transform.DeepFind("weaponHandleR").gameObject;
        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
