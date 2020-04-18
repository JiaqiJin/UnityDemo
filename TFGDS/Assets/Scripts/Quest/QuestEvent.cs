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
    public EventStatus status;

    public List<QuestPath> pathList = new List<QuestPath>();

    public QuestEvent(string n , string d)
    {
        id = Guid.NewGuid().ToString(); //identificador unico
        name = n;
        desc = d;
        status = EventStatus.WAITING;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
    }

    public string GetID()
    {
        return id;
    }
}
