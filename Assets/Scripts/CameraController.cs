using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The object to follow
    private Vector3 offset;  // Offset between the camera and the target

    void Start()
    {
        if (target != null)
        {
            // Calculate the initial offset
            offset = transform.position - target.position;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Maintain the relative position
            transform.position = target.position + offset;
        }
    }
}