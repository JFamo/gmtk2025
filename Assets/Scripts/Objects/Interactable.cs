using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private GameObject _player;

    private float promptTimeCheckInterval = 0.25f;
    private float lastPromptCheckTime = 0.0f;

    public static Interactable nearestInteractable;
    public static float nearestInteractableDistance = float.MaxValue;
    
    private void Update() {
        lastPromptCheckTime += Time.deltaTime;

        if (Input.GetKeyDown(GetKeyCode()) && GetDistanceFromPlayer() <= GetRange() &&
            (nearestInteractable == null || nearestInteractable == this))
        {
            HandleInteraction();
        }
        
        // Otherwise, every promptTimeCheckInterval seconds, check if the player is within range and show or hide the prompt text
        if (lastPromptCheckTime >= promptTimeCheckInterval || nearestInteractable == this) {
            lastPromptCheckTime = 0.0f;
            float distance = GetDistanceFromPlayer();
            if (distance <= GetRange())
            {
                // If this is the nearest interactable, set it as the nearestInteractable
                if (distance < nearestInteractableDistance)
                {
                    nearestInteractable = this;
                    nearestInteractableDistance = distance;
                }
                GameObject promptText = GetPromptText();
                if (promptText != null)
                {
                    promptText.SetActive(true);
                }
            }
            else
            {
                // If this is the nearest interactable, reset it
                if (nearestInteractable == this)
                {
                    nearestInteractable = null;
                    nearestInteractableDistance = float.MaxValue;
                }
                GameObject promptText = GetPromptText();
                if (promptText != null)
                {
                    promptText.SetActive(false);
                }
            }
        }
    }
    
    private GameObject GetPlayer() {
        if (_player is null) {
            _player = PlayerStateController.GetInstance().GetPlayerGameObject();
        }
        return _player;
    }

    private float GetDistanceFromPlayer()
    {
        GetPlayer();
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