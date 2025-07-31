using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {
    public int startingHealth = 3;
    
    private int health;
    private int drinks = 0;
    
    private static PlayerStateController _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Debug.LogWarning("Multiple instances of PlayerStateController detected. Using the existing instance.");
            Destroy(gameObject);
        }
    }

    void Start() {
        health = startingHealth;
    }
    
    public static PlayerStateController GetInstance() {
        if (_instance == null) {
            Debug.LogError("PlayerStateController singleton is null!");
        }
        return _instance;
    }
    
    public void AddDrink() {
        drinks++;
    }
    
    public int GetDrinks() {
        return drinks;
    }
    
    public void RemoveHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            HandlePlayerDeath();
        }
    }
    
    public int GetHealth() {
        return health;
    }
    
    public void AddHealth(int amount) {
        health += amount;
    }

    private void HandlePlayerDeath() {
        Debug.Log("!!!Player Died!!!");
    }
}
