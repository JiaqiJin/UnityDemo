using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();
    //List<QuestEvent> pathList = new List<QuestEvent>();

    public Quest()
    {

    }

    public QuestEvent AddQuestEvent(string n , string d,GameObject loc)
    {
        QuestEvent questEvent = new QuestEvent(n, d,loc);
        questEvents.Add(questEvent);
        return questEvent;
    }

    public void AddPath(string fromQustEvent , string toQuestEvent)
    {
        QuestEvent from = FindQuestEvent(fromQustEvent);
        QuestEvent to = FindQuestEvent(toQuestEvent);
        if(from != null && to != null)
        {
            QuestPath p = new QuestPath(from, to);
            from.pathList.Add(p);
        }

    }

    public void BFS(string id , int orderNumber = 1)
    {
        QuestEvent thisEvent = FindQuestEvent(id);
        thisEvent.order = orderNumber;

        foreach (QuestPath e in thisEvent.pathList)
        {
            if (e.endEvent.order == -1)
                BFS(e.endEvent.GetID(), orderNumber + 1);
        }
    }

    QuestEvent FindQuestEvent(string id)
    {
        foreach (QuestEvent n in questEvents)
        {
            if (n.GetID() == id)
                return n;
        }
        return null;
    }

    public void PrintPath()
    {
        foreach (QuestEvent n in questEvents)
        {
            Debug.Log(n.name + " " + n.order);
        }
    }

}
