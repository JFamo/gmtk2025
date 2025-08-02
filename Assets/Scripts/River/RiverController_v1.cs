using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Implementation of the river flow controller specific to a unit-circular shape.
// Eventually, let's put some gameobjects in the scene that define the flow then use some efficient way to find the K-Nearest Neighbors and average their flow.
public class RiverController_v1 : RiverController {
    public float baseSpeed = 0.5f;
    public float speedUpFactor = 1.2f;
    public Vector2 riverCenter = new Vector2(-38.0f, 10.0f);    
    
    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public override Vector2 GetRiverDirectionAtPoint(Vector3 point) {
        float dx = point.x - riverCenter.x;
        float dy = point.y - riverCenter.y;
        float hyp = Mathf.Sqrt(dx * dx + dy * dy);
        if (hyp < 0.01f) {
            hyp = 0.01f; // Avoid division by zero
        }
        Vector2 direction = new Vector2(dy / hyp, -dx / hyp);
        return direction;
    }

    public override float GetRiverForceAtPoint(Vector3 point) {
        return baseSpeed;
    }
    
    public override Vector2 GetRiverForceVectorAtPoint(Vector3 point) {
        Vector2 direction = GetRiverDirectionAtPoint(point);
        float force = GetRiverForceAtPoint(point);
        return direction * force;
    }

    public override void SpeedUp() {
        baseSpeed = baseSpeed * speedUpFactor;
        Debug.Log($"River speed increased to {baseSpeed}");
    }

    public override float GetBaseSpeed()
    {
        return baseSpeed;
    }
}