using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RiverController_v2 : RiverController {
    public float baseSpeed = 0.5f;
    public float speedUpFactor = 1.2f;
    
    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public override Vector2 GetRiverDirectionAtPoint(Vector3 point) {
        Debug.LogError("v2 river does not support GetRiverDirectionAtPoint");
        throw new NotImplementedException("GetRiverDirectionAtPoint is not implemented in RiverController_v");
    }

    public override float GetRiverForceAtPoint(Vector3 point) {
        Debug.LogError("v2 river does not support GetRiverForceAtPoint");
        throw new NotImplementedException("GetRiverForceAtPoint is not implemented in RiverController_v2");
    }
    
    public override Vector2 GetRiverForceVectorAtPoint(Vector3 point) {
        Debug.LogError("v2 river does not support GetRiverForceVectorAtPoint");
        throw new NotImplementedException("GetRiverForceVectorAtPoint is not implemented in RiverController");
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