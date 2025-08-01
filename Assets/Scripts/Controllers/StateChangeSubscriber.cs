using UnityEngine;

namespace Controllers {
    public abstract class StateChangeSubscriber : MonoBehaviour {
        public virtual void OnHealthChange(int newValue) {
            // By default, do nothing
        }

        public virtual void OnDrinkChange(int newValue) {
            // By default, do nothing
        }
        
        public virtual void OnLapChange(int newValue) {
            // By default, do nothing
        }
    }
}