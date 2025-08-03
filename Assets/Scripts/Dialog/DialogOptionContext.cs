using Quests;

namespace Dialog
{
    public class DialogOptionContext
    {
        private QuestKeys _questKey;
        private QuestStates _questState;
        private DialogInstance _followUpDialog;

        public DialogOptionContext(DialogInstance dialog)
        {
            _followUpDialog = dialog;
        }
        
        public DialogOptionContext(QuestKeys questKey, QuestStates questState, DialogInstance followUpDialog)
        {
            _questKey = questKey;
            _questState = questState;
            _followUpDialog = followUpDialog;
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

        public DialogInstance GetFollowUpDialog()
        {
            return _followUpDialog;
        }
    }
}