using UnityEngine;

namespace Obstacles {
    public class NoOpContactHandler : IContactHandler {
        public void HandleContact(GameObject other) {
            // Do nothing
        }
    }
}
