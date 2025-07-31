using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoController : MonoBehaviour
{
     public LineRenderer lineRenderer;
    public float maxDistance = 20f;
    public float springForce = 50f;
    public float springDamping = 5f;
    public KeyCode lassoKey = KeyCode.Mouse0;
    public KeyCode detachKey = KeyCode.Mouse1;

    private SpringJoint springJoint;
    private Rigidbody targetRb;
    private Transform lassoOrigin;

    void Start()
    {
        // The origin of the lasso is in this case the player
        lassoOrigin = this.transform; 
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(lassoKey))
        {
            TryLasso();
        }

        if (Input.GetKeyDown(detachKey))
        {
            DetachLasso();
        }
    }

    private void LateUpdate()
    {
        UpdateLineRenderer();
    }

    void TryLasso()
    {
        // Create a ray from the camera to the mouse cursor position
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Assumes ground is at y=0

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLookAt = cameraRay.GetPoint(rayLength);
            Vector3 direction = (pointToLookAt - lassoOrigin.position).normalized;

            // The actual ray for the lasso
            Ray lassoRay = new Ray(lassoOrigin.position, direction);

            if (Physics.Raycast(lassoRay, out RaycastHit hit, maxDistance))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Debug.Log("Lassoed Object: " + hit.collider.name);
                    AttachToTarget(hit.collider.GetComponent<Rigidbody>());
                }
                else
                {
                    // Lasso hits something non-interactable
                    Debug.Log("Hit Uninteractable Object: " + hit.collider.name);
                    ShowMissLasso(hit.point);
                }
            }
            else
            {
                // No hit at all
                Debug.Log("Lasso Missed");
                Vector3 missPoint = lassoOrigin.position + direction * maxDistance;
                ShowMissLasso(missPoint);
            }
        }
    }

    void ShowMissLasso(Vector3 endPoint)
    {
        StartCoroutine(ShowLassoLineTemporarily(endPoint));
    }

    IEnumerator ShowLassoLineTemporarily(Vector3 endPoint)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, lassoOrigin.position);
        lineRenderer.SetPosition(1, endPoint);

        yield return new WaitForSeconds(0.2f); // Show for 0.2 seconds

        if (springJoint == null) // Only clear if we didn't hit and attach
            lineRenderer.positionCount = 0;
    }

    void AttachToTarget(Rigidbody target)
    {
        DetachLasso(); // Remove any existing joint

        targetRb = target;

        // Add SpringJoint to the target
        springJoint = target.gameObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = null; // Attach to world point
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = lassoOrigin.position;

        springJoint.spring = springForce;
        springJoint.damper = springDamping;
        springJoint.maxDistance = Vector3.Distance(target.position, lassoOrigin.position);

        // Setup line renderer
        lineRenderer.positionCount = 2;
    }

    void DetachLasso()
    {
        if (springJoint != null)
        {
            Destroy(springJoint);
            springJoint = null;
        }

        targetRb = null;
        lineRenderer.positionCount = 0;
    }

    void UpdateLineRenderer()
    {
        if (targetRb != null && springJoint != null)
        {
            lineRenderer.SetPosition(0, lassoOrigin.position);
            lineRenderer.SetPosition(1, targetRb.position);

            // Update anchor in case player moves
            springJoint.connectedAnchor = lassoOrigin.position;
        }
    }
}
