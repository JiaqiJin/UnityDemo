using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyWeapon : MonoBehaviour
{

    public GameObject whR;
    private Collider weaponColR;
    // Start is called before the first frame update
    void Start()
    {
        whR = transform.DeepFind("WeaponR").gameObject;
        weaponColR = whR.GetComponent<Collider>();
    }

    public void WeaponEnable()
    {
        whR.SetActive(true);
    }

    public void WeaponDisable()
    {
        whR.SetActive(false);
    }
}
