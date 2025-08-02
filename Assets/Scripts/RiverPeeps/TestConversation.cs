using System.Collections.Generic;
using Controllers;
using Dialog;
using UnityEngine;

namespace Objects {
    public class TestConversation : Interactable {
        public float range;
        public KeyCode interactKey;
        public string myName;
        public string myDialog;
        public Sprite myPicture;
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return interactKey;
        }

        protected override void HandleInteraction() {
            DialogPanelController.GetInstance().LaunchDialog(
                new DialogInstance(myName, myPicture, myDialog, new List<DialogOption> {
                    new DialogOption("Howdy"),
                    new DialogOption("Never talk to me again")
                })
            );
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
