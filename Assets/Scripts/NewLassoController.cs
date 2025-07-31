using UnityEngine;

public class NewLassoController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float lassoMaxDistance = 10f;

    private Rigidbody2D playerRb;
    private SpringJoint2D springJoint;
    private Rigidbody2D targetRb;
    
    private Vector2 lastClickWorldPos;
    private bool isClicking;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        // Setup LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;

        // Setup SpringJoint2D
        springJoint = gameObject.AddComponent<SpringJoint2D>();
        springJoint.enabled = false;
        springJoint.autoConfigureDistance = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicking = true;
            lastClickWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(lastClickWorldPos);
            
            Debug.Log($"Clicked at world position: {lastClickWorldPos}");
            
            if (hit != null) // Set interactable tag on target objects
            {
                if (hit.CompareTag("Interactable"))
                {
                    Debug.Log($"Hit object: {hit.name}");

                    float distance = Vector2.Distance(transform.position, hit.transform.position);
                    Debug.Log($"Distance to object: {distance}");
                    if (distance <= lassoMaxDistance)
                    {
                        Debug.Log("Lassoed Object: " + hit.name);
                        AttachLasso(hit.GetComponent<Rigidbody2D>());
                    }
                }
                else
                {
                    Debug.Log("Lasso Missed: object not tagged as interactable");
                }
            }
            else
            {
                Debug.Log("Lasso Missed: no collider was hit");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClicking = false;
            DetachLasso();
        }

        // Always show line from player to last click
        Vector2 lineTarget = targetRb != null ? targetRb.position : lastClickWorldPos;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, lineTarget);
    }

    void AttachLasso(Rigidbody2D target)
    {
        targetRb = target;

        // Set spring joint
        springJoint.enabled = true;
        springJoint.connectedBody = targetRb;
        springJoint.distance = Vector2.Distance(transform.position, targetRb.position);
        springJoint.dampingRatio = 0.7f;
        springJoint.frequency = 2f;

        Debug.Log($"Lasso attached to {targetRb.name} at distance {springJoint.distance}");
    }

    void DetachLasso()
    {
        springJoint.enabled = false;
        springJoint.connectedBody = null;
        lineRenderer.enabled = false;
        targetRb = null;
    }
}