using UnityEngine;
using System.Collections;

public class LassoController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDistance = 20f;
    
    // Properties for SpringJoint2D
    public float springFrequency = 5f;
    public float springDampingRatio = 0.7f;

    public KeyCode lassoKey = KeyCode.Mouse0;
    public KeyCode detachKey = KeyCode.Mouse1;

    private SpringJoint2D springJoint;
    private Rigidbody2D targetRb;
    private Transform lassoOrigin;
    private Rigidbody2D playerRb; // The player's own Rigidbody2D

    void Start()
    {
        lassoOrigin = this.transform;
        // The player must have a Rigidbody2D for the joint to work.
        playerRb = GetComponent<Rigidbody2D>(); 
        if (playerRb == null)
        {
            Debug.LogError("LassoController requires a Rigidbody2D component on the player object.");
        }
        lineRenderer.positionCount = 0;
        // Ensure the LineRenderer is using world space
        lineRenderer.useWorldSpace = true;
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

    void LateUpdate()
    {
        UpdateLineRenderer();
    }

    void TryLasso()
    {
        // Create a ray from the camera to the mouse cursor position
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Create a plane at the player's z-position to ensure accurate aiming
        Plane gamePlane = new Plane(Vector3.forward, new Vector3(0, 0, lassoOrigin.position.z));

        if (gamePlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 worldPoint = cameraRay.GetPoint(rayLength);
            Vector2 direction = (new Vector2(worldPoint.x, worldPoint.y) - (Vector2)lassoOrigin.position).normalized;

            // Cast a 2D ray
            RaycastHit2D hit = Physics2D.Raycast(lassoOrigin.position, direction, maxDistance);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    Debug.Log("Lassoed Object: " + hit.collider.name);
                    AttachToTarget(hit.collider.GetComponent<Rigidbody2D>());
                }
                else
                {
                    Debug.Log("Hit Uninteractable Object: " + hit.collider.name);
                    ShowMissLasso(hit.point);
                }
            }
            else
            {
                Debug.Log("Lasso Missed");
                Vector2 missPoint = (Vector2)lassoOrigin.position + direction * maxDistance;
                ShowMissLasso(missPoint);
            }
        }
    }

    void ShowMissLasso(Vector2 endPoint)
    {
        StartCoroutine(ShowLassoLineTemporarily(endPoint));
    }

    IEnumerator ShowLassoLineTemporarily(Vector2 endPoint)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(lassoOrigin.position.x, lassoOrigin.position.y, 0));
        lineRenderer.SetPosition(1, new Vector3(endPoint.x, endPoint.y, 0));

        yield return new WaitForSeconds(0.2f);

        if (springJoint == null)
            lineRenderer.positionCount = 0;
    }

    void AttachToTarget(Rigidbody2D target)
    {
        if (target == null) return; // Don't attach if the target has no Rigidbody2D

        DetachLasso(); // Remove any existing joint

        targetRb = target;

        // Add SpringJoint2D to the player object and connect it to the target
        springJoint = gameObject.AddComponent<SpringJoint2D>();
        springJoint.connectedBody = targetRb;
        springJoint.autoConfigureDistance = false;
        springJoint.distance = Vector2.Distance(playerRb.position, targetRb.position);
        
        // Set spring properties
        springJoint.frequency = springFrequency;
        springJoint.dampingRatio = springDampingRatio;

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
            lineRenderer.SetPosition(0, new Vector3(lassoOrigin.position.x, lassoOrigin.position.y, 0));
            lineRenderer.SetPosition(1, new Vector3(targetRb.position.x, targetRb.position.y, 0));
        }
    }
}