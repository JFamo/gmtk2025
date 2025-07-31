
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float changeDirectionTime = 3.0f;

    private Vector3 randomDirection;
    private float timeSinceDirectionChange = 0.0f;

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        timeSinceDirectionChange += Time.deltaTime;

        if (timeSinceDirectionChange >= changeDirectionTime)
        {
            ChangeDirection();
            timeSinceDirectionChange = 0.0f;
        }

        transform.Translate(randomDirection * speed * Time.deltaTime, Space.World);
    }

    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        randomDirection = new Vector3(randomX, 0, randomZ).normalized;
    }
}
