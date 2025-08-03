using System.Collections.Generic;
using Controllers;
using Dialog;
using UnityEngine;

namespace Objects {
    public class SimpleConversation : Interactable {
        public float range;
        public Sprite myPicture;
        public string myName;
        public string myDialog;
        public string responseText;
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return KeyCode.E;
        }

        protected override void HandleInteraction() {
            DialogPanelController.GetInstance().LaunchDialog(
                new DialogInstance(myName, myPicture, myDialog, new List<DialogOption> {
                    new DialogOption(responseText)
                })
            );
        }

        protected override GameObject GetPromptText() {
            return null;
        }
    }
}
