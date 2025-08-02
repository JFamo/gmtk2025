using UnityEngine;

namespace Quests
{
    public class WaterGunKidQuestStateController : IQuestStateController
    {
        
        private QuestStates currentState = QuestStates.NOT_STARTED;

        public QuestStates GetCurrentState()
        {
            return currentState;
        }

        public QuestKeys GetKey()
        {
            return QuestKeys.WATER_GUN_KID_QUEST;
        }

        public void SetQuestState(QuestStates state)
        {
            currentState = state;
        }
        
    }
}