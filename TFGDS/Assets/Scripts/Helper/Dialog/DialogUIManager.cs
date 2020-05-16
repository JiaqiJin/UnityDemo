using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUIManager : MonoBehaviour
{
    public List<string> content;
    public DialogPanelUI panel;

    [SerializeField]
    private int currLine;
    // Start is called before the first frame update
    void Start()
    {
        Init();
   
    }

    // Update is called once per frame
    void Update()
    {
        //openg dialog
        if(Input.GetKeyDown("1"))
        {
            Init();
            showUI();
        }

        if(Input.GetKeyDown("2"))
        {
            NextLine();
            loadText(content[currLine]);
        }
    }

    private void Init()
    {
        hideUI();
        currLine = 0;
        panel.setContetText("");
    }

    void showUI()
    {
        panel.showCharaA(true);
        panel.showCharaB(true);
        panel.showContentBg(true);
        panel.showContexText(true);
    }

    void hideUI()
    {
        panel.showCharaA(false);
        panel.showCharaB(false);
        panel.showContentBg(false);
        panel.showContexText(false);
    }

    void NextLine()
    {
        currLine++;
    }

    void loadText(string value)
    {
        panel.setContetText(value);
    }

}
