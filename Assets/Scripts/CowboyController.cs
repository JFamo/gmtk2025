using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyController : MonoBehaviour
{
    // When the user presses one of WASD, nudge the cowboy in that direction using Unity's physics system.
    public float speed = 5f;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
        }

        // Apply the movement force
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }
}
