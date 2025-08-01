using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private GameObject _player;

    private float promptTimeCheckInterval = 0.25f;
    private float lastPromptCheckTime = 0.0f;
    
    private void Update() {
        lastPromptCheckTime += Time.deltaTime;
        
        if (Input.GetKeyDown(GetKeyCode()) && GetDistanceFromPlayer() <= GetRange())
        {
            HandleInteraction();
        }
        
        // Otherwise, every promptTimeCheckInterval seconds, check if the player is within range and show or hide the prompt text
        if (lastPromptCheckTime >= promptTimeCheckInterval) {
            lastPromptCheckTime = 0.0f;
            if (GetDistanceFromPlayer() <= GetRange())
            {
                GameObject promptText = GetPromptText();
                if (promptText != null)
                {
                    promptText.SetActive(true);
                }
            }
            else
            {
                GameObject promptText = GetPromptText();
                if (promptText != null)
                {
                    promptText.SetActive(false);
                }
            }
        }
    }

    private float GetDistanceFromPlayer() {
        if (_player is null) {
           _player = PlayerStateController.GetInstance().GetPlayerGameObject();
        }
        if (_player is null) {
            Debug.LogError("Interactable object failed to find any player object. Are they set on the PlayerStateController?");
            return float.MaxValue;
        }
        
        // Calculate distance in 2d
        Vector2 playerPosition = _player.transform.position;
        Vector2 interactablePosition = transform.position;
        return Vector2.Distance(playerPosition, interactablePosition);
    }

    protected abstract KeyCode GetKeyCode();

    protected abstract float GetRange();
    
    protected abstract void HandleInteraction();

    protected abstract GameObject GetPromptText();
}