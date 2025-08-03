using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class DrinkSpawner : StateChangeSubscriber
{
    
    public GameObject drinkPrefab;
    public GameObject emptyPrefab;
    public int minSpawns = 0;
    public int maxSpawns = 5;
    public float drinkRatio = 0.75f;
    public float spawnCircle = 2.0f;


    void Start()
    {
        DoSpawns();
    }
    
    public override void OnLapChange(int newValue)
    {
        DoSpawns();
    }

    private void DoSpawns()
    {
        int newSpawns = Random.Range(minSpawns, maxSpawns + 1);
        int drinkSpawns = Mathf.RoundToInt(newSpawns * drinkRatio);
        SpawnObjects(drinkSpawns, true);
        SpawnObjects(newSpawns - drinkSpawns, false);
    }
    
    private void SpawnObjects(int spawns, bool drink)
    {
        // Choose a random vector3 within a unit circle of my transform.position
        Vector2 rand = Random.insideUnitCircle * spawnCircle;
        Vector3 spawn = (Vector2)transform.position + rand;
        spawn.z = drinkPrefab.transform.position.z;
        for (int i = 0; i < spawns; i++)
        {
            Instantiate(drink ? drinkPrefab : emptyPrefab, spawn, Quaternion.identity);
        }
    }
}
