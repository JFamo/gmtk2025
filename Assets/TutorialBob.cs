using UnityEngine;

public class TutorialBob : MonoBehaviour
{
    public float angleRange = 15f; // Maximum angle in degrees
    public float speed = 1f;       // Bobbing speed

    private float startZ;

    void Start()
    {
        startZ = transform.localEulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * angleRange;
        Vector3 euler = transform.localEulerAngles;
        euler.z = startZ + angle;
        transform.localEulerAngles = euler;
    }
}