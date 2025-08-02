using System;
using System.Collections;
using System.Collections.Generic;
using River;
using Unity.VisualScripting;
using UnityEngine;

public class RiverObject : MonoBehaviour {
    private Rigidbody2D rb;
    private Collider2D col;
    
    private RiverFlowZone currentFlowZone;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        FindCurrentFlowZone();
    }

    private void FindCurrentFlowZone()
    {
        // Use OverlapCollider to detect zones overlapping at spawn
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.SetLayerMask(Physics2D.DefaultRaycastLayers);
        filter.useLayerMask = true;

        Collider2D[] results = new Collider2D[10];
        int count = col.OverlapCollider(filter, results);

        for (int i = 0; i < count; i++)
        {
            RiverFlowZone zone = results[i].GetComponent<RiverFlowZone>();
            if (zone != null)
            {
                currentFlowZone = zone;
                break;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        CheckAndSetFlowZone(other);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FlowZone"))
        {
            RiverFlowZone zone = other.GetComponent<RiverFlowZone>();
            if (zone != null && zone == currentFlowZone)
            {
                currentFlowZone = null;
            }
        }
    }

    private void CheckAndSetFlowZone(Collider2D other)
    {
        if (other.CompareTag("FlowZone"))
        {
            RiverFlowZone zone = other.GetComponent<RiverFlowZone>();
            if (zone == null)
            {
                Debug.LogError("Some object has tag FlowZone without attached RiverFlowZone component!");
            }
            currentFlowZone = zone;
        }
    }

    void Update()
    {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Current Flow Zone is null: " + currentFlowZone.IsUnityNull());
        }
    }
    
    private void FixedUpdate() {
        if (currentFlowZone.IsUnityNull()) return;
        rb.AddForce(currentFlowZone.GetFlow(), ForceMode2D.Force);
    }
}
