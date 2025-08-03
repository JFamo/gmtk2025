using System.Collections.Generic;
using Controllers;
using Dialog;
using UnityEngine;

namespace Objects {
    public class TutorialConversation : Interactable {
        public float range;
        public Sprite lifeguardPicture;
        public Sprite myPicture;

        private LaunchDialogOptionSelectHandler launchDialogOptionSelectHandler = new LaunchDialogOptionSelectHandler();
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return KeyCode.E;
        }

        protected override void HandleInteraction()
        {
            DialogPanelController.GetInstance().LaunchDialog(
                GetStartDialog());
        }

        private DialogInstance GetStartDialog()
        {
            return new DialogInstance("Larry", lifeguardPicture,
                "Saaah dude! You look like a totally tubular cowboy dude who I've just met, but can I let you in on a little secret?",
                new List<DialogOption>
                {
                    new DialogOption("Howdy", launchDialogOptionSelectHandler,
                        new DialogOptionContext(GetGoalDialog())
                    )
                });
        }
        
        private DialogInstance GetGoalDialog()
        {
            return new DialogInstance("Larry", lifeguardPicture,
                "We've got this little conveniently loop-themed challenge here. The fastest lifeguard to complete 5 laps in the pool wins our super ripper prize...",
                new List<DialogOption>
                {
                    new DialogOption("...", launchDialogOptionSelectHandler,
                        new DialogOptionContext(GetDrinksDialog())
                    )
                });
        }
        
        private DialogInstance GetDrinksDialog()
        {
            return new DialogInstance("Larry", lifeguardPicture,
                "...and for each drink they can pound while doing it, they'll get, like, 5 seconds off their time.",
                new List<DialogOption>
                {
                    new DialogOption("One thing...", launchDialogOptionSelectHandler,
                        new DialogOptionContext(GetCloseDialog())
                    )
                });
        }
        
        private DialogInstance GetCloseDialog()
        {
            return new DialogInstance("Soon-to-be-drunk Cowboy", myPicture,
                "I don't much care for getting this hat wet. So I'll try your challenge, but three little splashes on this here Stetson and I'm out",
                new List<DialogOption>
                {
                    new DialogOption("Let's go!")
                });
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
