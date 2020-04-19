using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManage : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox;
    public GameObject buttonPrefab;

    public GameObject A;
    public GameObject B;
    public GameObject C;

    private void Start()
    {
        QuestEvent a = quest.AddQuestEvent("test1", "desc 1",A);
        QuestEvent b = quest.AddQuestEvent("test2", "desc 2",B);
        QuestEvent c = quest.AddQuestEvent("test3", "desc 3",C);

        quest.AddPath(a.GetID(), b.GetID());
        quest.AddPath(b.GetID(), c.GetID());

        quest.BFS(a.GetID());
        //quest a
        QuestButton button = CreateButton(a).GetComponent<QuestButton>();
        A.GetComponent<QuestLocation>().SetUp(this, a, button);
        //quest b 
        button = CreateButton(b).GetComponent<QuestButton>();
        B.GetComponent<QuestLocation>().SetUp(this, b, button);
        //quest c 
        button = CreateButton(c).GetComponent<QuestButton>();
        C.GetComponent<QuestLocation>().SetUp(this,c , button);


        //quest.PrintPath();
    }

    GameObject CreateButton(QuestEvent e)
    {
        GameObject b = Instantiate(buttonPrefab);
        b.GetComponent<QuestButton>().SetUp(e, questPrintBox);
        if(e.order == 1)
        {
            b.GetComponent<QuestButton>().UpdateButton(QuestEvent.EventStatus.CURRENT);
            e.status = QuestEvent.EventStatus.CURRENT;
        }
        return b;
    }

    public void UpdateQuestComplete(QuestEvent e)
    {
        foreach (QuestEvent n in quest.questEvents)
        {
            if(n.order == (e.order + 1))
            {
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT); //cambiar al siguente quest statu
            }
        }
    }

}
