using UnityEngine;

namespace River
{
    public class RiverFlowZone : MonoBehaviour
    {
        public float speedMultiplier = 1.0f;

        public Vector2 GetFlow()
        {
            // Get my right direction
            return transform.right.normalized * (RiverController.GetInstance().GetBaseSpeed() * speedMultiplier);
        }
    }
}