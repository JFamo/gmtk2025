using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers {
    public class LapStateController : MonoBehaviour {

        public int totalMarkers = 2;
        
        private int _currentLap = 0;
        private int _currentMarker = 0;
        
        // For the lap speed-up feature
        private bool _willSpeedUpNextLap;
        
        private List<StateChangeSubscriber> _subscribers = new List<StateChangeSubscriber>();
        
        private static LapStateController _instance;
        public static LapStateController GetInstance() {
            if (_instance == null) {
                Debug.LogError("LapStateController singleton is null! Please ensure it is initialized in the scene.");
            }
            return _instance;
        }
        
        public void Subscribe(StateChangeSubscriber subscriber) {
            if (!_subscribers.Contains(subscriber)) {
                _subscribers.Add(subscriber);
            }
        }
        
        public void Unsubscribe(StateChangeSubscriber subscriber) {
            if (_subscribers.Contains(subscriber)) {
                _subscribers.Remove(subscriber);
            }
        }

        public void NotifyMarkerPassed(int markerIndex) {
            if (markerIndex == _currentMarker + 1) {
                _currentMarker = markerIndex;
            }

            if (_currentMarker == totalMarkers) {
                CompleteLap();
            } else if (_currentMarker > totalMarkers) {
                Debug.LogError("This should never happen - how did you go too far");
            }
        }

        private void CompleteLap() {
            Debug.Log($"Lap {_currentLap} completed!");
            if (_willSpeedUpNextLap) {
                RiverController.GetInstance().SpeedUp();
            }
            _currentLap += 1;
            _currentMarker = 0;
            _willSpeedUpNextLap = true;
            NotifyLapChangeSubscribers();
        }
        
        private void NotifyLapChangeSubscribers() {
            foreach (var subscriber in _subscribers) {
                subscriber.OnLapChange(_currentLap);
            }
        }

        void Awake() {
            if (_instance == null) {
                _instance = this;
            }
            else {
                Debug.LogError("Multiple instances of LapStateController detected. Using the existing instance.");
                Destroy(this);
            }
        }

        public int GetLapNumber() {
            return _currentLap;
        }

        public void SetSpeedUp(bool speedUp) {
            this._willSpeedUpNextLap = speedUp;
        }
        
    }
}