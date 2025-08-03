using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class SpawnSittingDrink : StateChangeSubscriber
{
    public float spawnChance = 0.3f;
    public GameObject drinkPrefab; // Assign in the inspector or load dynamically


    void Start()
    {
        DoRandomDrinkSpawn();
    }
    public override void OnLapChange(int newValue)
    {
        DoRandomDrinkSpawn();
    }

    private void DoRandomDrinkSpawn()
    {
        // If spawn chance is met, spawn a drink
        if (Random.value < spawnChance)
        {
            // Instantiate the drink prefab at the calculated position
            if (drinkPrefab != null)
            {
                Instantiate(drinkPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
