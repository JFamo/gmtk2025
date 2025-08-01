using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LassoController : MonoBehaviour
{
    [Tooltip("The maximum distance the grapple can reach.")]
    public float lassoMaxDistance = 10f;
    [Tooltip("How long the line shows when a grapple misses.")]
    public float missShotDisplayDurationSecs = 0.25f;

    [Header("Spring Joint Settings")]
    [Tooltip("The frequency of the spring. Higher values are stiffer.")]
    public float springFrequency = 0.5f;
    [Tooltip("The damping ratio of the spring. 0 is bouncy, 1 is critical damping.")]
    [Range(0f, 1f)]
    public float springDampingRatio = 0.5f;
    [Tooltip("The resting distance of the spring for civilians")]
    public float civilianSpringDistance = 3f;
    [Tooltip("The resting distance of the spring for collectibles")]
    public float collectibleSpringDistance = 1f;
    
    private LineRenderer lineRenderer;
    private SpringJoint2D springJoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        springJoint = null;

        // Setup LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowLasso();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DetachLasso();
        }

        if (springJoint)
        {
            UpdateLineRenderer();
        }
    }

    void ThrowLasso()
    {
        if (springJoint) return;
        
        float distanceToObjects = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distanceToObjects;
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        if (hit.collider != null) // Set interactable tag on target objects
        {
            if (hit.collider.CompareTag("Civilian") || hit.collider.CompareTag("Collectible"))
            {
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);
                Debug.Log($"Clicked object: {hit.collider.name} {distance} units away");
                if (distance <= lassoMaxDistance)
                {
                    AttachLasso(hit.collider.GetComponent<Rigidbody2D>());
                    return;
                }
            }
            else
            {
                Debug.Log($"Lasso Hit un-interactable object: {hit.collider.name}");
            }
        }
        else
        {
            Debug.Log("Lasso Missed: no collider was hit");
        }
        // Object too far, object uninteractable, or no object hit
        StartCoroutine(ShowMissedShot(clickPosition));
    }

    void AttachLasso(Rigidbody2D targetRb)
    {
        // Set spring joint
        springJoint = gameObject.AddComponent<SpringJoint2D>();
        springJoint.autoConfigureDistance = false;
        springJoint.connectedBody = targetRb;
        // Set spring properties for the "pulling" lasso effect
        springJoint.distance = targetRb.CompareTag("Civilian") ? civilianSpringDistance : collectibleSpringDistance;
        springJoint.dampingRatio = springDampingRatio;
        springJoint.frequency = springFrequency;
        // Draw the line renderer
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetRb.position);

        Debug.Log($"Lasso attached to {targetRb.name}");
    }

    void DetachLasso()
    {
        if (springJoint)
        {
            Destroy(springJoint);
            lineRenderer.enabled = false;
            Debug.Log("Lasso detached");
        }
    }

    void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, springJoint.connectedBody.transform.position);
    }
    
    IEnumerator ShowMissedShot(Vector3 clickPosition)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        
        float distance = Vector3.Distance(transform.position, clickPosition);
        if (distance <= lassoMaxDistance)
        {
            lineRenderer.SetPosition(1, clickPosition);
        }
        else
        {
            Vector3 direction = (clickPosition - transform.position).normalized;
            lineRenderer.SetPosition(1, transform.position + direction * lassoMaxDistance);
        }

        yield return new WaitForSeconds(missShotDisplayDurationSecs);

        // Only hide the line if the player hasn't successfully grappled in the meantime
        if (!springJoint)
        {
            lineRenderer.enabled = false;
        }
    }
}