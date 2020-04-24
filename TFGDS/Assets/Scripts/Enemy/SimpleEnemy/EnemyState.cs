using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

    public int HP;
    public int MP;
    public float attack;
    public float defence;
    public float attackDistance;
    public float skillDistance;

    private bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
        isDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public bool IsDeath
    {
        get { return isDeath; }
    }

    public void TakeDamage(string str, bool isCritical = false)
    {
        this.HP -= int.Parse(str);
        if (this.HP < 0)
            isDeath = true;
    }

}
