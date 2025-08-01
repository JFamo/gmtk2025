using System.Collections;
using System.Collections.Generic;
using Controllers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {
    public int startingHealth = 3;
    
    private int health;
    private int drinks = 0;
    
    private List<StateChangeSubscriber> subscribers = new List<StateChangeSubscriber>();
    
    private static PlayerStateController _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            health = startingHealth;
        } else {
            Debug.LogWarning("Multiple instances of PlayerStateController detected. Using the existing instance.");
            Destroy(gameObject);
        }
    }
    
    public static PlayerStateController GetInstance() {
        if (_instance == null) {
            Debug.LogError("PlayerStateController singleton is null!");
        }
        return _instance;
    }

    public void Subscribe(StateChangeSubscriber subscriber) {
        if (!subscribers.Contains(subscriber)) {
            subscribers.Add(subscriber);
        }
    }
    
    public void Unsubscribe(StateChangeSubscriber subscriber) {
        if (subscribers.Contains(subscriber)) {
            subscribers.Remove(subscriber);
        }
    }
    
    public void AddDrink() {
        drinks++;
        NotifyDrinkChangeSubscribers();
    }
    
    public int GetDrinks() {
        return drinks;
    }
    
    public void RemoveHealth(int amount) {
        health -= amount;
        // TODO - could be bugs if we don't reverse these?
        NotifyHealthChangeSubscribers();
        if (health <= 0) {
            HandlePlayerDeath();
        }
    }
    
    public int GetHealth() {
        return health;
    }
    
    public void AddHealth(int amount) {
        health += amount;
        NotifyHealthChangeSubscribers();
    }

    private void NotifyHealthChangeSubscribers() {
        foreach (var subscriber in subscribers) {
            subscriber.OnHealthChange(health);
        }
    }
    
    private void NotifyDrinkChangeSubscribers() {
        foreach (var subscriber in subscribers) {
            subscriber.OnDrinkChange(drinks);
        }
    }

    private void HandlePlayerDeath() {
        Debug.Log("!!!Player Died!!!");
    }
}
