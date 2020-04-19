using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvent 
{
    //Enumerado para especificar el estado del eventos
    public enum EventStatus { WAITING, CURRENT, DONE};

    public string name;
    public string desc;
    public string id;
    public int order = -1;
    public QuestButton button;
    public EventStatus status;
    public GameObject location;

    public List<QuestPath> pathList = new List<QuestPath>();

    public QuestEvent(string n , string d , GameObject loc)
    {
        id = Guid.NewGuid().ToString(); //identificador unico
        name = n;
        desc = d;
        status = EventStatus.WAITING;
        location = loc;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
        button.UpdateButton(es);
    }

    public string GetID()
    {
        return id;
    }
}
