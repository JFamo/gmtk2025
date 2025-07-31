using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverCivilian_v1 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ApplyRandomForce());
    }

    IEnumerator ApplyRandomForce()
    {
        while (true)
        {
            // Wait for a random time between 1 and 3 seconds
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            // Generate a random direction
            Vector3 direction = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f // Movement is on the XY plane
            ).normalized;

            // Apply the force
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
    }
}