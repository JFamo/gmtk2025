using UnityEngine;

public class DrinkBob : MonoBehaviour
{
    private float startZ;
    private float phase = 0f;

    void Start()
    {
        startZ = transform.localEulerAngles.z;
    }

    void Update()
    {
        int drinks = PlayerStateController.GetInstance().GetDrinks();
        phase += GetSpeed(drinks) * Time.deltaTime;
        float angle = Mathf.Sin(phase) * GetAngleRange(drinks);
        Vector3 euler = transform.localEulerAngles;
        euler.z = startZ + angle;
        transform.localEulerAngles = euler;
    }

    private float GetSpeed(int drinks)
    {
        // Speed: 0x for 0 drinks, 2x for 30+ drinks
        return Mathf.Lerp(0f, 2f, Mathf.Clamp01(drinks / 30f));
    }

    private float GetAngleRange(int drinks)
    {
        // Angle: 0 for 0 drinks, 15 for 30+ drinks
        return Mathf.Lerp(0f, 15f, Mathf.Clamp01(drinks / 30f));
    }
}