using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public Button buttonComponent;
    public RawImage icon;
    public Text eventName;
    public Sprite currentImage;
    public Sprite waitingImage;
    public Sprite doneImage;
    public QuestEvent thisEvent;
    public CompasControl compassController;

    QuestEvent.EventStatus status;

    private void Awake()
    {
        buttonComponent.onClick.AddListener(ClickHandler);
        //GameObject canvasChild = GameObject.Find("Canvas").gameObject;
        //compassController = canvasChild.transform.Find("Compas____").GetComponent<CompasControl>();
        compassController = GameObject.Find("Compass").GetComponent<CompasControl>();
    }

    public void SetUp(QuestEvent e, GameObject scrollList)
    {
        thisEvent = e;
        buttonComponent.transform.SetParent(scrollList.transform, false);
        eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.desc;
        status = thisEvent.status;
        icon.texture = waitingImage.texture;
        buttonComponent.interactable = false;
    }

    public void UpdateButton(QuestEvent.EventStatus s)
    {
        status = s;
        if(status == QuestEvent.EventStatus.DONE)
        {
            icon.texture = doneImage.texture;
            buttonComponent.interactable = false;
            
        }
        else if (status == QuestEvent.EventStatus.WAITING)
        {
            icon.texture = waitingImage.texture;
            buttonComponent.interactable = false;
        }
        else if (status == QuestEvent.EventStatus.CURRENT)
        {
            icon.texture = currentImage.texture;
            buttonComponent.interactable = true;
            //ClickHandler();
        }
    }
    //set compas contro al puntos de localizacion
    public void ClickHandler()
    {
        compassController.target = thisEvent.location;
    }
}
