using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyController : MonoBehaviour
{
    [Header("Movement Config")]
    public float speed = 5f;

    [Header("Drunk Config")] 
    public int drunkThreshold = 0; // Number of drinks to start applying drunk modifiers
    public float drunkSpeedMultiplier = 0.95f;
    public float drunkDirectionMultiplier = 0.5f; // How much the direction is randomized when drunk
    public float drunkRotationMultiplier = 1.0f; // How much the rotation is randomized when drunk
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement() {
        int drinks = PlayerStateController.GetInstance().GetDrinks();
        
        Vector3 direction = Vector3.zero;
        float effectiveSpeed = 0.0f;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3.up;
            effectiveSpeed = speed;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.down;
            effectiveSpeed = speed;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
            effectiveSpeed = speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
            effectiveSpeed = speed;
        }
        
        // Apply some randomness if the player is drunk
        if (drinks > drunkThreshold)
        {
            direction += new Vector3(
                Random.Range(-drunkDirectionMultiplier, drunkDirectionMultiplier),
                Random.Range(-drunkDirectionMultiplier, drunkDirectionMultiplier),
                0f
            );
            effectiveSpeed *= Mathf.Pow(drunkSpeedMultiplier, drinks);
            
            // Apply some slight rotational force directly correlated to the number of drinks and speed
            float rotationForce = drinks * drunkRotationMultiplier * effectiveSpeed;
            rb.AddTorque(rotationForce * Random.Range(-1f, 1f), ForceMode2D.Impulse);
        }
        
        // Apply the movement force
        rb.AddForce(direction.normalized * effectiveSpeed, ForceMode2D.Impulse);
    }
}
