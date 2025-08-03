using UnityEngine;

namespace Quests
{
    public class AlienQuestStateController : GenericQuestStateController
    {
        public override QuestKeys GetKey()
        {
            return QuestKeys.ALIEN_QUEST;
        }
    }
}