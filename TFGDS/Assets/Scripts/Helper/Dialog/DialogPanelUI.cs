using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogPanelUI : MonoBehaviour
{
    public RawImage charAImage;
    public RawImage charBImage;
    public Image contentBg;
    public Text contentText;
    public List<GameObject> panelUI;
    public GameObject btnA;
    public GameObject btnB;
    public void ShowCanvas(bool value)
    {
        //panelUI.SetActive(value);
        foreach (GameObject game in panelUI)
        {
            game.SetActive(value);
        }
    }

    public void SetButtonNames(string nameA,string nameB)
    {
        btnA.name = nameA;
        btnB.name = nameB;
    }

    public void ShowButton(bool value)
    {
        btnA.SetActive(value);
        btnB.SetActive(value);
    }

    public void SetButtonText(string texA,string texB)
    {
        Text tempTex = btnA.GetComponentInChildren<Text>();

        tempTex.text = texA;
        tempTex = btnB.GetComponentInChildren<Text>();
        tempTex.text = texB;
    }

    public void showCharaA(bool value)
    {
        charAImage.enabled = value;
    }

    public void showCharaB(bool value)
    {
        charBImage.enabled = value;
    }

    public void showContentBg(bool value)
    {
        contentBg.enabled = value;
    }

    public void showContexText(bool value)
    {
        contentText.enabled = value;
    }

    public void setContetText(string text)
    {
        contentText.text = text;
    }

    public void ChangeCharaATex(Texture tex)
    {
        charAImage.texture = tex;
    }

    public void ChangeCharaBTex(Texture tex)
    {
        charBImage.texture = tex;
    }

}
