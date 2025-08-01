using TMPro;

namespace Controllers.ui {
    public class LapDisplayController : StateChangeSubscriber {
        public TMP_Text lapText;

        private void ShowLapText(int lap) {
            lapText.text = "Lap: " + lap;
        }

        void Start() {
            ShowLapText(1);
            LapStateController.GetInstance().Subscribe(this);
        }
        
        public override void OnLapChange(int newValue) {
            ShowLapText(newValue + 1);
        }
    }
}