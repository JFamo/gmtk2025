using UnityEngine;

namespace Obstacles {
    public class DestroyOtherHandler : IContactHandler {
        public void HandleContact(GameObject other) {
            Object.Destroy(other);
        }
    }
}