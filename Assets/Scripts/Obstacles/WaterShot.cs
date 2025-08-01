using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UnityEngine;

public class WaterShot : Obstacle {
    public float contactCooldownSeconds = 1.0f;

    private IContactHandler _contactHandler;
    private float speed;
    private int damage;

    void Start() {
        _contactHandler = new DamagePlayerHandler(damage);
    }

    void Update() {
        // Move in forward direction each step
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }
    
    public void Create(int damage, float speed, float lifetime) {
        this.damage = damage;
        this.speed = speed;
        StartCoroutine(DestroyAfterSeconds(lifetime));
    }
    
    private IEnumerator DestroyAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    
    protected override IContactHandler GetPlayerContactHandler() {
        return _contactHandler;
    }

    protected override float GetContactCooldownSeconds() {
        return contactCooldownSeconds;
    }
}