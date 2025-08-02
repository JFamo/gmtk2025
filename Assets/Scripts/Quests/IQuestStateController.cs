namespace Quests
{
    public interface IQuestStateController
    {
        public QuestKeys GetKey();
        
        public void SetQuestState(QuestStates state);
    }

    public enum QuestKeys
    {
        WATER_GUN_KID_QUEST
    }
}