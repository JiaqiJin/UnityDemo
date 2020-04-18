using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManage : MonoBehaviour
{
    public Quest quest = new Quest();

    private void Start()
    {
        QuestEvent a = quest.AddQuestEvent("test1", "desc 1");
        QuestEvent b = quest.AddQuestEvent("test2", "desc 2");
        QuestEvent c = quest.AddQuestEvent("test3", "desc 3");

        quest.AddPath(a.GetID(), b.GetID());
        quest.AddPath(b.GetID(), c.GetID());

        quest.PrintPath();
    }

}
