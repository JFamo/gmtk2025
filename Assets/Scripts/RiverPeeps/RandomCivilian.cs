using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCivilian : MonoBehaviour
{
    public Sprite[] possibleSkins;

    void Start()
    {
        SetRandomSprite();
    }

    private void SetRandomSprite()
    {
        // Choose a random sprite from the possibleSkins array
        if (possibleSkins.Length > 0)
        {
            int randomIndex = Random.Range(0, possibleSkins.Length);
            SpriteRenderer spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = possibleSkins[randomIndex];
            }
            else
            {
                Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("No skins available to choose from.");
        }
    }
}
