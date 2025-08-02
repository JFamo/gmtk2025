using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RiverController : MonoBehaviour {
    protected static RiverController _instance;

    public static RiverController GetInstance() {
        if (_instance.IsUnityNull()) {
            Debug.LogError("RiverController singleton is null!");
        }

        return _instance;
    }

    public abstract Vector2 GetRiverDirectionAtPoint(Vector3 point);

    public abstract float GetRiverForceAtPoint(Vector3 point);

    public abstract Vector2 GetRiverForceVectorAtPoint(Vector3 point);

    public abstract void SpeedUp();
    
    public abstract float GetBaseSpeed();
}