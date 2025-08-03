using System.Collections.Generic;
using Controllers;
using Dialog;
using Quests;
using UnityEngine;

namespace Objects {
    public class MonkConversation : Interactable {
        public float range = 10.0f;
        public Sprite myPicture;

        private string myName = "Mysterious Monk";
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return KeyCode.E;
        }

        protected override void HandleInteraction()
        {
            DialogInstance dialog;
            QuestStates questState = QuestCoordinator.GetInstance().GetQuestStateController(QuestKeys.MONK_QUEST).GetQuestState();
            if (questState == QuestStates.IN_PROGRESS)
            {
                dialog = GetCost();
            } else if (questState == QuestStates.COMPLETED || questState == QuestStates.FAILED)
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
                "For many years *hiccup* I've dedicated my life to the study of Mai Tai.",
                new List<DialogOption>
                {
                    new DialogOption("I think you mean Muay Thai", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetWisdom())),
                    new DialogOption("And what have you learned?", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetWisdom()))
                });
        }
        
        private DialogInstance GetPassive()
        {
            return new DialogInstance(myName, myPicture,
                "The wisdom of Mai Tai is *hiccup* truly... inspiring",
                new List<DialogOption>
                {
                    new DialogOption("Very wise")
                });
        }
        
        private DialogInstance GetWisdom()
        {
            return new DialogInstance(myName, myPicture,
                "Through the study of Mai Tai I have gained the incredible ability to drink Mai Tai.",
                new List<DialogOption>
                {
                    new DialogOption("Oh", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetWaterBending())),
                    new DialogOption("Incredible", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetWaterBending()))
                });
        }
        
        private DialogInstance GetWaterBending()
        {
            return new DialogInstance(myName, myPicture,
                "I've also gained the incredible ability to bend water.",
                new List<DialogOption>
                {
                    new DialogOption("Oh", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetCost())),
                    new DialogOption("Incredible", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetCost()))
                });
        }
        
        private DialogInstance GetCost()
        {
            var options = new List<DialogOption>();
            var successHandlers = new List<IDialogOptionSelectHandler>
            {
                new SetQuestStateDialogOptionHandler(),
                new RemoveDrinkOptionSelectHandler(),
                new RemoveDrinkOptionSelectHandler(),
                new RemoveDrinkOptionSelectHandler(),
                new RemoveDrinkOptionSelectHandler(),
                new RemoveDrinkOptionSelectHandler()
            };
            if (PlayerStateController.GetInstance().GetDrinks() > 4)
            {
                options.Add(new DialogOption("Let's do it", successHandlers, new DialogOptionContext(QuestKeys.MONK_QUEST, QuestStates.COMPLETED, null)));
                options.Add(new DialogOption("I'm going to study these Mai Tai myself", new SetQuestStateDialogOptionHandler(), new DialogOptionContext(QuestKeys.MONK_QUEST, QuestStates.FAILED, null)));
            }
            else
            {
                options.Add(new DialogOption("I don't have 5 drinks right now", new SetQuestStateDialogOptionHandler(), new DialogOptionContext(QuestKeys.MONK_QUEST, QuestStates.IN_PROGRESS, null)));
            }
            return new DialogInstance(myName, myPicture,
                "I can use this ability to disable some fountains, in *hiccup* exchange for 5 Mai Tai.",
                options);
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
