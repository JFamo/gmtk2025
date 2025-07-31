using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverObject : MonoBehaviour {
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb.AddForce(RiverController.GetInstance().GetRiverForceVectorAtPoint(transform.position), ForceMode2D.Force);
    }
}
