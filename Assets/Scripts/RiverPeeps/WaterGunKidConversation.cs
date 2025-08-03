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
        public string myDialog;
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
            return new DialogInstance(myName, myPicture, myDialog, new List<DialogOption>
            {
                new DialogOption("never play with water guns ever again",
                    new List<IDialogOptionSelectHandler>
                        { new SetQuestStateDialogOptionHandler(), new LaunchDialogOptionSelectHandler() },
                    new DialogOptionContext(QuestKeys.WATER_GUN_KID_QUEST, QuestStates.COMPLETED, SuccessDialog())),
                new DialogOption("oh ok cool",
                    new List<IDialogOptionSelectHandler>
                        { new SetQuestStateDialogOptionHandler(), new LaunchDialogOptionSelectHandler() },
                    new DialogOptionContext(QuestKeys.WATER_GUN_KID_QUEST, QuestStates.FAILED, FailureDialog())),
            });
        }

        private QuestStates GetQuestState()
        {
            return QuestCoordinator.GetInstance().GetQuestStateController(QuestKeys.WATER_GUN_KID_QUEST)
                .GetQuestState();
        }

        private DialogInstance SuccessDialog()
        {
            return new DialogInstance(myName, myPicture, "Ok, since you seem like such a cool and easy-going person.",
                new List<DialogOption>
                {
                    new DialogOption("Ok goodbye.")
                });
        }

        private DialogInstance FailureDialog()
        {
            return new DialogInstance(myName, myPicture, "No way old man! We're gonna play with guns!",
                new List<DialogOption>
                {
                    new DialogOption("Ok goodbye.")
                });
        }

        protected override GameObject GetPromptText()
        {
            return null;
        }
    }
}