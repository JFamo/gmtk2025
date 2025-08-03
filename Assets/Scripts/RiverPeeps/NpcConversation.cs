using System.Collections.Generic;
using System.Diagnostics;
using Controllers;
using Dialog;
using UnityEngine;

namespace Objects {
    public class NpcConversation : Interactable {
        public float range;
        public string myName;
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return KeyCode.E;
        }

        private string GetDialogText()
        {
            // based on how many drinks the player has, return a different dialog
            int drinks = PlayerStateController.GetInstance().GetDrinks();
            if (drinks < 5)
            {
                // Return one of a random set of dialogs
                string[] dialogs = {
                    "How's it going?",
                    "What's up?",
                    "Wow! A real life cowboy!",
                    "Is that a lasso?",
                    "Nice hat!"
                };
                return dialogs[Random.Range(0, dialogs.Length)];
            }
            else if (drinks < 10)
            {
                // Return one of a random set of dialogs
                string[] dialogs = {
                    "Watch where you're going!",
                    "Let's keep this party going!",
                    "Nice hat!",
                    "What's up?",
                    "You look like you could use another drink!",
                    "Where are you going in a hurry?"
                };
                return dialogs[Random.Range(0, dialogs.Length)];
            }
            else 
            {
                // Return one of a random set of dialogs
                string[] dialogs = {
                    "Didn't I just see you?",
                    "Okay, one more tequila sunrise",
                    "I'm pretty sure I've seen you drink 3 pina coladas in the last 5 minutes",
                    "This is the best day ever!",
                    "I love it here",
                    "Nice hat!"
                };
                return dialogs[Random.Range(0, dialogs.Length)];
            }
        }

        private List<DialogOption> GetOptions()
        {
            int drinks = PlayerStateController.GetInstance().GetDrinks();
            if (drinks < 10)
            {
                return new List<DialogOption>
                {
                    new DialogOption("Howdy"),
                    new DialogOption("Out of my way!")
                };
            }
            else if (drinks < 20)
            {
                return new List<DialogOption>
                {
                    new DialogOption("... howdy ..."),
                    new DialogOption("I like your tube")
                };
            }
            else 
            {
                return new List<DialogOption>
                {
                    new DialogOption("...howdee ..."),
                    new DialogOption("Let's do shots")
                };
            }
        }

        protected override void HandleInteraction()
        {
            DialogPanelController.GetInstance().LaunchDialog(
                new DialogInstance(myName, GetComponentsInChildren<SpriteRenderer>()[0].sprite, GetDialogText(),
                    GetOptions()
                ));
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
