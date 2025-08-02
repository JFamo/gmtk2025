using Quests;
using UnityEngine;

namespace Dialog
{
    public class SetQuestStateDialogOptionHandler : IDialogOptionSelectHandler
    {
        public void HandleOptionSelected(DialogOptionContext context)
        {
            // Get quest coordinator
            var questCoordinator = QuestCoordinator.GetInstance();
            // Get quest key and state from context
            var questKey = context.GetQuestKey();
            var questState = context.GetQuestState();
            // Check if quest key and state are null, error if so
            if (questKey == null || questState == null)
            {
                Debug.LogError("Quest key or state is null in SetQuestStateDialogOptionHandler.");
                return;
            }
            // Get the quest state controller for the specified quest key
            var questStateController = questCoordinator.GetQuestStateController(questKey);
            // If the quest state controller is null, log an error
            if (questStateController == null)
            {
                Debug.LogError($"Quest state controller for {questKey} not found in SetQuestStateDialogOptionHandler.");
                return;
            }
            questStateController.SetQuestState(questState);
        }
    }
}