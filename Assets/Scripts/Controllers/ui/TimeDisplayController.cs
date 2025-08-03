using TMPro;
using UnityEngine;

namespace Controllers.ui {
    public class TimeDisplayController : MonoBehaviour {
        public TMP_Text timeText;

        private void ShowTimeText() {
            // Shows the time elapsed since game start in minutes:seconds format
            float timeElapsed = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);
            timeText.text = $"{minutes:D2}:{seconds:D2}";
        }

        private void Update()
        {
            ShowTimeText();
        }
    }
}