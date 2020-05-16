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

}
