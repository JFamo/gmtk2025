namespace Quests
{
    public interface IQuestStateController
    {
        public QuestKeys GetKey();
        
        public void SetQuestState(QuestStates state);

        public QuestStates GetQuestState();
    }

    public enum QuestKeys
    {
        WATER_GUN_KID_QUEST
    }
}