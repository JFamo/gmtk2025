using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UnityEngine;

public class WaterFeature : Obstacle {
    public int damage = 1;
    public float contactCooldownSeconds = 1.0f;

    private IContactHandler _contactHandler;

    void Start() {
        _contactHandler = new DamagePlayerHandler(damage);
    }
    
    protected override IContactHandler GetPlayerContactHandler() {
        return _contactHandler;
    }

    protected override float GetContactCooldownSeconds() {
        return contactCooldownSeconds;
    }
}