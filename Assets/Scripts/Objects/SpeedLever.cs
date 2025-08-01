using Controllers;
using UnityEngine;

namespace Objects {
    public class SpeedLever : Interactable {
        public float range;
        public KeyCode interactKey;
        public GameObject promptText;
    
        protected override float GetRange() {
            return range;
        }
    
        protected override KeyCode GetKeyCode() {
            return interactKey;
        }

        protected override void HandleInteraction() {
            LapStateController.GetInstance().SetSpeedUp(false);
        }

        protected override GameObject GetPromptText() {
            return promptText;
        }
    }
}
