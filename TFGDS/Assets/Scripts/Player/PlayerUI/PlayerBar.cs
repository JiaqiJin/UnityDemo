using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    public Image healthBar;
    public Image mpBar;
    public Image staminaBar;
    public Text healthText;
    public Text mpText;
    public Text staminText;
    private void Awake()
    {
        healthText = transform.Find("HealthBar/Text").gameObject.GetComponent<Text>();
        mpText = transform.Find("ManaBar/Text").gameObject.GetComponent<Text>();
        staminText = transform.Find("GrennBar/Text").gameObject.GetComponent<Text>();

        healthBar = transform.Find("HealthBar/Health").gameObject.GetComponent<Image>();
        mpBar = transform.Find("ManaBar/Health").gameObject.GetComponent<Image>();
        staminaBar = transform.Find("GrennBar/Health").gameObject.GetComponent<Image>();
        PlayerInfo.instance_.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
       
    }


    private void Update()
    {
        PlayerInfo info = PlayerInfo.instance_;
        healthBar.fillAmount = info.HP / 100.0f;
        mpBar.fillAmount = info.MP / 100.0f;
        staminaBar.fillAmount = info.Stamina / 100.0f;
        healthText.text = info.HP + "/100";
        mpText.text = info.MP + "/100";
        staminText.text = info.Stamina + "/100";
    }
    private void Start()
    {
        PlayerInfo.instance_.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    } 
    private void OnDestroy()
    {
        PlayerInfo.instance_.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }
    // actializacion del jugador cuando se cambia algun valor 
    void OnPlayerInfoChanged(InfoType type)
    {
       if(type == InfoType.All|| type == InfoType.Level || type == InfoType.MP || type == InfoType.HP || type == InfoType.MP)
        {
            UpdateShow();
            //Debug.Log("update");
        }
            
        //type == InfoType.Level|| type == InfoType.MP
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo.instance_;
        healthBar.fillAmount = info.HP / 100.0f;
        mpBar.fillAmount = info.MP / 100.0f;
        staminaBar.fillAmount = info.Stamina / 100.0f;
        healthText.text = info.HP + "/100";
        mpText.text = info.MP + "/100";
        staminText.text = info.Stamina + "/100";
        //Debug.Log(info.HP);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
           
            PlayerInfo.instance_.Stamina -= 20;
            //Debug.Log(PlayerInfo.instance_.HP);
        }
    }

}
