using System.Collections.Generic;
using Controllers;
using Dialog;
using UnityEngine;

namespace Objects {
    public class GamblerConversation : Interactable {
        public float range = 10.0f;
        public Sprite myPicture;
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return KeyCode.E;
        }

        private string GetText()
        {
            // Return one of a random set of options
            string[] dialogs = {
                "I'm playing some roulette on my phone right now. Red just hit twice, so black's due... what do you think?",
                "I'm playing some roulette on my phone right now. I feel like it has to be red. what do you think?",
                "Online roulette - I'm about to go all-in. Red or black?"
            };
            return dialogs[Random.Range(0, dialogs.Length)];
        }

        protected override void HandleInteraction() {
            DialogPanelController.GetInstance().LaunchDialog(
                new DialogInstance("Kenny", myPicture, GetText(), new List<DialogOption> {
                    new DialogOption("Red", new LaunchDialogOptionSelectHandler(), new DialogOptionContext(GetFollowUp())),
                    new DialogOption("Black", new LaunchDialogOptionSelectHandler(), new DialogOptionContext(GetFollowUp()))
                })
            );
        }

        private DialogInstance GetFollowUp()
        {
            // With chance 1 in 2, say red, else say black
            bool isRed = Random.Range(0.0f, 2.0f) < 1.0f;
            string followUpText = isRed ? "I knew it! It was red!" : "It was black!";

            return new DialogInstance("Kenny", myPicture, followUpText, new List<DialogOption>
            {
                new DialogOption("Gamble responsibly, partner")
            });
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
