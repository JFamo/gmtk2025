using UnityEngine;

namespace Quests
{
    public class MonkQuestStateController : GenericQuestStateController
    {
        public override QuestKeys GetKey()
        {
            return QuestKeys.MONK_QUEST;
        }
    }
}