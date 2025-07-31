using UnityEngine;

namespace Obstacles {
    public interface IContactHandler {
        public abstract void HandleContact(GameObject other);
    }
}