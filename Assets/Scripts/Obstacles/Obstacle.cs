using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {
    private float _lastContactTime;
    private IContactHandler noOpContactHandler = new NoOpContactHandler();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding is the player
        if (collision.CompareTag("Player")) {
            // Check if the cooldown period has passed
            if (Time.time - _lastContactTime >= GetContactCooldownSeconds()) {
                _lastContactTime = Time.time; // Update the last contact time
                GetPlayerContactHandler().HandleContact(collision.gameObject);
            }
        }
        if (collision.CompareTag("Civilian")) {
            GetCivilianContactHandler().HandleContact(collision.gameObject);
        }
    }
    
    protected abstract float GetContactCooldownSeconds();

    protected virtual IContactHandler GetPlayerContactHandler() {
        return noOpContactHandler;
    }

    protected virtual IContactHandler GetCivilianContactHandler() {
        return noOpContactHandler;
    }
}
