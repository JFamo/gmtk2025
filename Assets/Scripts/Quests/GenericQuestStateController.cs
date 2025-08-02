namespace Quests
{
    public abstract class GenericQuestStateController : IQuestStateController
    {
        private QuestStates _currentState = QuestStates.NOT_STARTED;

        public QuestStates GetCurrentState()
        {
            return _currentState;
        }
        
        public abstract QuestKeys GetKey();

        public void SetQuestState(QuestStates state)
        {
            _currentState = state;
        }
        
        public QuestStates GetQuestState()
        {
            return _currentState;
        }
    }
}