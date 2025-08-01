using UnityEngine;

namespace Controllers.Laps {
    public class LapTracker : MonoBehaviour {

        public int markerNumber;
    
        private LapStateController _lapStateController;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) {
                Debug.Log("Player collided with markerNumber " + markerNumber);
                UpdateLapState();
            }
        }

        private void UpdateLapState() {
            if (_lapStateController == null) {
                _lapStateController = LapStateController.GetInstance();
            }
            _lapStateController.NotifyMarkerPassed(markerNumber);
        }
    }
}
