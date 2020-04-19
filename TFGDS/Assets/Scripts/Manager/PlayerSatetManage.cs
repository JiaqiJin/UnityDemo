using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatetManage : IActorManagerInterface
{

    //public AnimatorManager am;
    
    private int health;
    private int restHealth = 10;
    [Header("------------flag--------")]
    public bool isGround;
    public bool isAttack;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    private void Awake()
    {
        //player = PlayerInfo.instance_;
        //PlayerInfo.instance_.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }

    private void Update()
    {
        PlayerInfo player = PlayerInfo.instance_;
        health = player.HP;

        isGround = am.ac.CheckState("ground");
        //Debug.Log(isGround);
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isRoll = am.ac.CheckState("roll");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackL");
        isHit = am.ac.CheckState("hit");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        isDefense = am.ac.CheckState("defense1h", "defense");
    }

    public void Test()
    {
        Debug.Log(health);
    }

    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.HP || type == InfoType.MP)
        {
            UpdatePlayer();
            //Debug.Log("update");
        }

        //type == InfoType.Level|| type == InfoType.MP
    }
    public void UpdatePlayer()
    {
        PlayerInfo info = PlayerInfo.instance_;
        health = info.HP;
    }

}
