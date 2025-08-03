using System.Collections.Generic;
using Controllers;
using Dialog;
using Quests;
using UnityEngine;

namespace Objects {
    public class CowgirlConversation : Interactable {
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
            QuestStates questState = QuestCoordinator.GetInstance().GetQuestStateController(QuestKeys.COWGIRL_QUEST).GetQuestState();
            if (questState == QuestStates.COMPLETED)
            {
                dialog = PlayerStateController.GetInstance().GetHealth() < 3 ? GetHeal() : GetRomance();
            }
            else if(questState == QuestStates.FAILED)
            {
                dialog = GetHate();
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
                "Well howdy partner! I sure didn't expect to see a fella like you out here.",
                new List<DialogOption>
                {
                    new DialogOption("Howdy! Nice to meet you", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetNice())),
                    new DialogOption("I've had " + PlayerStateController.GetInstance().GetDrinks() + " drinks", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetDrunk()))
                });
        }
        
        private DialogInstance GetRomance()
        {
            return new DialogInstance(myName, myPicture,
                "Good to see you again, partner",
                new List<DialogOption>
                {
                    new DialogOption("Golly")
                });
        }

        private DialogInstance GetHeal()
        {
            return new DialogInstance(myName, myPicture,
                "Let me dry off that hat for you, partner",
                new List<DialogOption>
                {
                    new DialogOption("Thank you kindly", new AddHealthOptionSelectHandler(), new DialogOptionContext())
                });
        }
        
        private DialogInstance GetHate()
        {
            return new DialogInstance(myName, myPicture,
                "Are you... still chugging drinks and paddling in circles around this lazy river?",
                new List<DialogOption>
                {
                    new DialogOption("Yes ma'am")
                });
        }
        
        private DialogInstance GetDrunk()
        {
            return new DialogInstance(myName, myPicture,
                "Oh, wow",
                new List<DialogOption>
                {
                    new DialogOption("I'm trying to go as fast as I can and do 5 laps on this lazy river", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetFail()))
                });
        }
        
        private DialogInstance GetFail()
        {
            return new DialogInstance(myName, myPicture,
                "Oh, okay...",
                new List<DialogOption>
                {
                    new DialogOption("I think I love you", new SetQuestStateDialogOptionHandler(), new DialogOptionContext(QuestKeys.COWGIRL_QUEST, QuestStates.FAILED, null))
                });
        }
        
        private DialogInstance GetNice()
        {
            return new DialogInstance(myName, myPicture,
                "Can I just say, I find it so impressive how you've been drinking a ton and paddling in circles around this lazy river",
                new List<DialogOption>
                {
                    new DialogOption("Oh thank you", new LaunchDialogOptionSelectHandler(),
                        new DialogOptionContext(GetSuccess()))
                });
        }
        
        private DialogInstance GetSuccess()
        {
            DialogInstance followUp = PlayerStateController.GetInstance().GetHealth() < 3 ? GetHeal() : null;
            var handlers = new List<IDialogOptionSelectHandler>
            {
                new SetQuestStateDialogOptionHandler()
            };
            if (followUp != null)
            {
                handlers.Add(new LaunchDialogOptionSelectHandler());
            }
            return new DialogInstance(myName, myPicture,
                "I don't know, it just feels like we're somehow meant to be here together. I hope I see you again real soon.",
                new List<DialogOption>
                {
                    new DialogOption("Well golly", handlers, new DialogOptionContext(QuestKeys.COWGIRL_QUEST, QuestStates.COMPLETED, followUp))
                });
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
