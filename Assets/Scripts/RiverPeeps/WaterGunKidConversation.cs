using System.Collections.Generic;
using Controllers;
using Dialog;
using Quests;
using UnityEngine;

namespace Objects
{
    public class WaterGunkidConversation : Interactable
    {
        public float range;
        public string myName;
        public Sprite myPicture;

        protected override float GetRange()
        {
            return range;
        }

        protected override KeyCode GetKeyCode()
        {
            return KeyCode.E;
        }

        protected override void HandleInteraction()
        {
            DialogInstance dialog;
            if (GetQuestState() == QuestStates.NOT_STARTED)
            {
                dialog = ChallengeDialog();
            }
            else if (GetQuestState() == QuestStates.COMPLETED)
            {
                dialog = SuccessDialog();
            }
            else
            {
                dialog = FailureDialog();
            }
            DialogPanelController.GetInstance().LaunchDialog(
                dialog
            );
        }

        private DialogInstance ChallengeDialog()
        {
            var successOption = new DialogOption("If I give you a drink, will you play somewhere else?",
                new List<IDialogOptionSelectHandler>
                    { new SetQuestStateDialogOptionHandler(), new RemoveDrinkOptionSelectHandler(), new LaunchDialogOptionSelectHandler() },
                new DialogOptionContext(QuestKeys.WATER_GUN_KID_QUEST, QuestStates.COMPLETED, SuccessDialog()));
            var options = new List<DialogOption>
            {

                new DialogOption("Don't play with guns, kid",
                    new List<IDialogOptionSelectHandler>
                        { new SetQuestStateDialogOptionHandler(), new LaunchDialogOptionSelectHandler() },
                    new DialogOptionContext(QuestKeys.WATER_GUN_KID_QUEST, QuestStates.FAILED, FailureDialog())),
            };
            if(PlayerStateController.GetInstance().GetDrinks() > 0)
            {
                options.Add(successOption);
            }
            return new DialogInstance(myName, myPicture, "My Mom said I can have a Mr. Beast Feastable if I leave her alone for 10 minutes!", options);
        }

        private QuestStates GetQuestState()
        {
            return QuestCoordinator.GetInstance().GetQuestStateController(QuestKeys.WATER_GUN_KID_QUEST)
                .GetQuestState();
        }

        private DialogInstance SuccessDialog()
        {
            return new DialogInstance(myName, myPicture, "Gee, thanks for this drink that suddenly doesn't have an alcohholic connotation!",
                new List<DialogOption>
                {
                    new DialogOption("Bye, kid")
                });
        }

        private DialogInstance FailureDialog()
        {
            return new DialogInstance(myName, myPicture, "Who shows up to a water park in a full cowboy outfit anyway?",
                new List<DialogOption>
                {
                    new DialogOption("...")
                });
        }

        protected override GameObject GetPromptText()
        {
            return null;
        }
    }
}