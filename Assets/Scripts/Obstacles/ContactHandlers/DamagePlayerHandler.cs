using UnityEngine;

namespace Obstacles {
    public class DamagePlayerHandler : IContactHandler {
        private int damage;
        
        public DamagePlayerHandler(int damage) {
            this.damage = damage;
        }
        
        public void HandleContact(GameObject other) {
            PlayerStateController.GetInstance().RemoveHealth(damage);
        }
    }
}
