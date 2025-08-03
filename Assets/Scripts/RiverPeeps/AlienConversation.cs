using System.Collections.Generic;
using Controllers;
using Dialog;
using Quests;
using UnityEngine;

namespace Objects {
    public class AlienConversation : Interactable {
        public float range = 10.0f;
        public Sprite myPicture;
        public string myName;
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return KeyCode.E;
        }

        protected override void HandleInteraction()
        {
            DialogInstance dialog;
            QuestStates questState = QuestCoordinator.GetInstance().GetQuestStateController(QuestKeys.ALIEN_QUEST).GetQuestState();
            if (questState == QuestStates.COMPLETED || questState == QuestStates.FAILED)
            {
                dialog = GetPassive();
            }
            else
            {
                dialog = GetInitial();
            }
            DialogPanelController.GetInstance().LaunchDialog(
                dialog
            );
        }

        private DialogInstance GetInitial()
        {
            return new DialogInstance(myName, myPicture,
                "WE HAVE COME IN SEARCH OF PINA COLADA",
                new List<DialogOption>
                {
                    new DialogOption("Yea I've got some", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetDemand())),
                    new DialogOption("Why?", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetDemand()))
                });
        }
        
        private DialogInstance GetPassive()
        {
            return new DialogInstance(myName, myPicture,
                "YOU HAVE FAILED TO PROVIDE PINA COLADA. MILLIONS WILL SUFFER.",
                new List<DialogOption>
                {
                    new DialogOption("Dagnabbit")
                });
        }
        
        private DialogInstance GetDemand()
        {
            return new DialogInstance(myName, myPicture,
                "GIVE US 100,000 PINA COLADA",
                new List<DialogOption>
                {
                    new DialogOption("Slow down, partner", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetFail())),
                    new DialogOption("I don't have 100,000 pina colada", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetFail()))
                });
        }
        
        private DialogInstance GetFail()
        {
            return new DialogInstance(myName, myPicture,
                "THE CONSEQUENCES FOR YOUR SPECIES WILL BE DIRE",
                new List<DialogOption>
                {
                    new DialogOption("Dagnabbit", new SetQuestStateDialogOptionHandler(), new DialogOptionContext(QuestKeys.ALIEN_QUEST, QuestStates.FAILED, null)),
                    new DialogOption("Oh okay", new SetQuestStateDialogOptionHandler(), new DialogOptionContext(QuestKeys.ALIEN_QUEST, QuestStates.FAILED, null))
                });
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
