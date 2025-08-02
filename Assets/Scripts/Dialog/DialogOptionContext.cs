using Quests;

namespace Dialog
{
    public class DialogOptionContext
    {
        private QuestKeys _questKey;
        private QuestStates _questState;
        
        public DialogOptionContext(QuestKeys questKey, QuestStates questState)
        {
            _questKey = questKey;
            _questState = questState;
        }

        public DialogOptionContext()
        {
            // Set no context
        }
        
        public QuestKeys GetQuestKey()
        {
            return _questKey;
        }
        
        public QuestStates GetQuestState()
        {
            return _questState;
        }
    }
}