using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding is the player
        if (collision.CompareTag("Player")) {
            HandleCollected();
            Destroy(gameObject);
        }
    }

    protected abstract void HandleCollected();
}