using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UnityEngine;

public class Bomb : Obstacle {
    public int damage = 3;
    public float contactCooldownSeconds = 0.0f;

    private IContactHandler _damagePlayerHandler;
    private IContactHandler _destroyCivilianHandler;

    void Start() {
        _damagePlayerHandler = new DamagePlayerHandler(damage);
        _destroyCivilianHandler = new DestroyOtherHandler();
    }
    
    protected override IContactHandler GetPlayerContactHandler() {
        return _damagePlayerHandler;
    }

    protected override IContactHandler GetCivilianContactHandler() {
        return _destroyCivilianHandler;
    }

    protected override float GetContactCooldownSeconds() {
        return contactCooldownSeconds;
    }
}