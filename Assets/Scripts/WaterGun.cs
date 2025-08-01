using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour {
    public float shotInterval;
    public float shotSpeed;
    public float shotLifetime;
    public GameObject waterBulletPrefab;
    
    private float timeSinceLastShot = 0.0f;

    void Start() {
        ResetShotWithRandomDelay();
    }
    
    void Update() {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shotInterval) {
            Shoot();
            ResetShotWithRandomDelay();
        }
    }
    
    private void ResetShotWithRandomDelay() {
        // Reset the time since the last shot with a random delay
        timeSinceLastShot = Random.Range(0.0f, shotInterval / 2);
    }
    
    private void Shoot() {
        // Instantiate the water bullet at the position and rotation of the water gun
        GameObject waterBullet = Instantiate(waterBulletPrefab, transform.position, transform.rotation);
        waterBullet.GetComponent<WaterShot>().Create(1, shotSpeed, shotLifetime);
    }
}
