using System.Collections;
using System.Collections.Generic;
using Controllers;
using Quests;
using UnityEngine;

public class QuestConditionalObject : StateChangeSubscriber {
    public QuestKeys questKey;
    public bool appearsAfterQuest;
    public QuestStates appearState;
    public bool disappearAfterQuest;
    public QuestStates disappearState;
    
    private void Start() {
        LapStateController.GetInstance().Subscribe(this);
        if(appearsAfterQuest) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }

    public override void OnLapChange(int newValue) {
        if (appearsAfterQuest && QuestCoordinator.GetInstance().GetQuestStateController(questKey).GetQuestState() == appearState) {
            Debug.Log("Setting active from quest " + questKey);
            gameObject.SetActive(true);
        }
        else if (disappearAfterQuest && QuestCoordinator.GetInstance().GetQuestStateController(questKey).GetQuestState() == disappearState) {
            Debug.Log("Setting inactive from quest " + questKey);
            gameObject.SetActive(false);
        }
    }
}
