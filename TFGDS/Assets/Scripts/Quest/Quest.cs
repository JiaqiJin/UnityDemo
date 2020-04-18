using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();
    List<QuestEvent> pathList = new List<QuestEvent>();

    public Quest()
    {

    }

    public QuestEvent AddQuestEvent(string n , string d)
    {
        QuestEvent questEvent = new QuestEvent(n, d);
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
            Debug.Log(n.name);
        }
    }

}
