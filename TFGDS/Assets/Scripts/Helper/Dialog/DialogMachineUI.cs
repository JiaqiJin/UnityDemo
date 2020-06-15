using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMachineUI : MonoBehaviour
{
    public enum STATE
    {
        OFF,
        TYPING,
        PAUSED,
        CHOICES
    }

    public STATE state;
    
    private bool justEnter = false;

    //public List<string> content;
    public Story01 data;
    public DialogConfig assert;
    public DialogPanelUI panel;

    [Range(1,100)]
    public float typingSpeed = 15.0f;

    [SerializeField]
    private int currLine;

    private string targetString;
    private float timerValue;
    // Start is called before the first frame update
    void Start()
    {
        //Init();
        state = STATE.OFF;
        justEnter = true;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case STATE.OFF:
                if (justEnter)
                {
                    //off state
                    //Init ui panel
                    Init();
                    LoadCharaTexture(assert.charATex, assert.charBTex);
                    justEnter = false;
                }                
                break;
            case STATE.TYPING:
                if (justEnter)
                {
                    //otyping state
                    //Load contetn
                    showUI();
                    LoadContent(data.dataList[currLine].Dialogtext.ToString(), data.dataList[currLine].Charaadisplay,
                                  data.dataList[currLine].Charabdisplay);
                    justEnter = false;
                    timerValue = 0;
                }
                CheckTypingFinish();
                UpdateContentString();
                break;
            case STATE.PAUSED:
                if (justEnter)
                {
                    //paused state
                    justEnter = false;
                }
                break;
            case STATE.CHOICES:
                if (justEnter)
                {
                    //paused state
                    //Init the 2 buttons
                    panel.ShowButton(true);
                    panel.SetButtonNames(data.dataList[currLine].Btnamsg, data.dataList[currLine].Btnbmsg);
                    panel.SetButtonText(data.dataList[currLine].Btnatex, data.dataList[currLine].Btnbtex);
                    justEnter = false;
                }
                break;
            default:
                break;
        }
       
    }

    public void StartDialog()
    {
        if(state == STATE.OFF)
        {
            PlayerInfo.instance_.CursoModeState = true;
            GoToState(STATE.TYPING);
        }    
    }

    public void UserClicked()
    {
        
        switch (state)
        {
            case STATE.TYPING:
                GoToState(STATE.OFF);
                break;
            case STATE.PAUSED:
                NextLine();
                if (currLine >= data.dataList.Count)
                {
                    //fisnih dialog
                    GoToState(STATE.OFF);
                    //dialog finish
                    PlayerInfo.instance_.CursoModeState = false;
                }
                else
                {
                    GoToState(STATE.TYPING);
                }
                break;
            default:
                break;
        }
    }

    private void CheckTypingFinish()
    {
        if(state == STATE.TYPING)
        {
            if((int)Mathf.Floor(timerValue * typingSpeed) >= targetString.Length)
            {
                if(data.dataList[currLine].Ischoise)
                {
                    GoToState(STATE.CHOICES);
                    //return;
                }
                else
                {
                    GoToState(STATE.PAUSED);
                }
               
            }
            //GoToState(STATE.PAUSED);
        }
    }

    private void GoToState(STATE next)
    {
        state = next;
        justEnter = true;
    }

    private void Init()
    {
        hideUI();
        currLine = 0;
        panel.setContetText("");
        LoadContent(data.dataList[currLine].Dialogtext.ToString(), data.dataList[currLine].Charaadisplay,
                data.dataList[currLine].Charabdisplay);
        panel.ShowButton(false);
    }

    void showUI()
    {
       
        panel.ShowCanvas(true);
    }

    void hideUI()
    {
        
        panel.ShowCanvas(false);
    }

    void NextLine()
    {
        currLine++;
    }

    /*void loadText(string value)
    {
        panel.setContetText(value);
    }
    */
    void LoadContent(string value,bool charAdisplay, bool charBdisplay)
    {
        //panel.setContetText(value);
        targetString = value;
        panel.showCharaA(charAdisplay);
        panel.showCharaB(charBdisplay);
    }

    void LoadCharaTexture(Texture charAtex, Texture charBtex)
    {
        panel.ChangeCharaATex(charAtex);
        panel.ChangeCharaBTex(charBtex);
    }

    void UpdateContentString()
    {
        timerValue += Time.deltaTime;
        string tempString = targetString.Substring(0,Mathf.Min((int)Mathf.Floor(timerValue * typingSpeed),targetString.Length));
        panel.setContetText(tempString);
    }

    public void ProcessBtnMSG(GameObject game)
    {
        switch (game.name)
        {
            case "GoHome":
                Story01 tempStory = Resources.Load<Story01>("Story02_");
                data = tempStory;

                Init();
                justEnter = false;
                LoadCharaTexture(assert.charATex, assert.charBTex);
                GoToState(STATE.TYPING);
                break;
            case "Yes":
                GameObject.FindGameObjectWithTag("AI Visual").SendMessage("OpenDoor");
                GoToState(STATE.OFF);
                break;
            default:
                break;
        }
    }

}
