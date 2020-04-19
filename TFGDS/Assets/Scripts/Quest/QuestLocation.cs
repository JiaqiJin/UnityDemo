using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    public QuestManage qManager;
    public QuestEvent qEvent;
    public QuestButton qButton;

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag != "Player") return;
        if (qEvent.status != QuestEvent.EventStatus.CURRENT) return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestComplete(qEvent);
        
    }

    /*private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag != "Player") return;
        if (qEvent.status != QuestEvent.EventStatus.CURRENT) return;

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestComplete(qEvent);

        Debug.Log("enter");
    }
    */
    public void SetUp(QuestManage qm , QuestEvent qe , QuestButton qb)
    {
        qEvent = qe;
        qManager = qm;
        qButton = qb;
        qe.button = qButton;
    }
}
