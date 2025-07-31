using UnityEngine;

public class LassoController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDistance = 20f;
    public float springForce = 50f;
    public float springDamping = 5f;
    public KeyCode lassoKey = KeyCode.Mouse0;
    public KeyCode detachKey = KeyCode.Space;

    private SpringJoint springJoint;
    private Rigidbody targetRb;
    private Transform lassoOrigin;

    void Start()
    {
        // Optional: define the origin point of the lasso (e.g., a child empty GameObject)
        lassoOrigin = this.transform; // or assign in Inspector
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

        UpdateLineRenderer();
    }

    void TryLasso()
    {
        Ray ray = new Ray(lassoOrigin.position, lassoOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Lassoed Object: " + hit.collider.name);
                AttachToTarget(hit.collider.GetComponent<Rigidbody>());
            }
        }
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