using UnityEngine;

namespace Quests
{
    public class CowgirlQuestStateController : GenericQuestStateController
    {
        public override QuestKeys GetKey()
        {
            return QuestKeys.COWGIRL_QUEST;
        }
    }
}