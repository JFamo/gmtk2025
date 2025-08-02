using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LassoController : MonoBehaviour
{
    [Tooltip("The maximum distance the lasso can reach.")]
    public float lassoMaxDistance = 10f;
    [Tooltip("How fast the lasso line extends outwards.")]
    public float lassoCastSpeed = 50f;
    [Tooltip("How long the line shows when a lasso misses.")]
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

    [Header("Animation Sprites v1")] 
    public Sprite passiveSprite;
    public Sprite lassoSprite;
    
    private LineRenderer lineRenderer;
    private SpringJoint2D springJoint;
    private Coroutine lassoCastCoroutine;
    private bool isLassoing;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        springJoint = null;

        // Setup LineRenderer
        lineRenderer.positionCount = 2;
        SetLassoing(false);
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
    
    // Function to set sprite based on lasso state
    public void SetLassoSprite(bool isLassoing)
    {
        if (isLassoing)
        {
            GetComponentsInChildren<SpriteRenderer>()[0].sprite = lassoSprite;
        }
        else
        {
            GetComponentsInChildren<SpriteRenderer>()[0].sprite = passiveSprite;
        }
    }

    void ThrowLasso()
    {
        if (lassoCastCoroutine != null || springJoint != null) return;

        Rigidbody2D targetRb = null; // Target object to attach the lasso to, if caught
        
        float distCameraToObjects = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distCameraToObjects;
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        float distanceThrown = Vector2.Distance(transform.position, clickPosition);
        if (hit.collider != null) // Set interactable tag on target objects
        {
            if (hit.collider.CompareTag("Civilian") || hit.collider.CompareTag("Collectible"))
            {
                Debug.Log($"Clicked object: {hit.collider.name} {distanceThrown} units away");
                if (distanceThrown <= lassoMaxDistance)
                {
                    targetRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
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

        if (distanceThrown > lassoMaxDistance)
        {
            Vector3 lassoDirection = (clickPosition - transform.position).normalized;
            Vector3 endPoint = transform.position + lassoDirection * lassoMaxDistance;
            lassoCastCoroutine = StartCoroutine(AnimateLasso(endPoint, targetRb));
        }
        else
        {
            lassoCastCoroutine = StartCoroutine(AnimateLasso(clickPosition, targetRb));
        }
    }

    IEnumerator AnimateLasso(Vector3 clickPos, Rigidbody2D targetRb)
    {
        SetLassoing(true);
        Vector2 startPoint = transform.position;
        Vector2 currentPosition = startPoint;
        
        float elapsedTime = 0f;
        float timeToTarget = Vector2.Distance(startPoint, clickPos) / lassoCastSpeed;
        
        while (elapsedTime < timeToTarget)
        {
            // Update the line's start and end points during animation
            lineRenderer.SetPosition(0, transform.position);
            currentPosition = Vector2.Lerp(startPoint, clickPos, elapsedTime / timeToTarget);
            lineRenderer.SetPosition(1, currentPosition);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Ensure the line reaches the exact target position
        lineRenderer.SetPosition(1, clickPos);
        // If we hit an object, create the joint
        if (targetRb != null)
        {
            // Set spring joint
            springJoint = gameObject.AddComponent<SpringJoint2D>();
            springJoint.autoConfigureDistance = false;
            springJoint.connectedBody = targetRb;
            // Set spring properties for the "pulling" lasso effect
            springJoint.distance = targetRb.CompareTag("Civilian") ? civilianSpringDistance : collectibleSpringDistance;
            springJoint.dampingRatio = springDampingRatio;
            springJoint.frequency = springFrequency;
        }
        else
        {
            yield return new WaitForSeconds(missShotDisplayDurationSecs);
            SetLassoing(false);
        }
        
        // Mark the coroutine as finished
        lassoCastCoroutine = null;
    }

    void DetachLasso()
    {
        if (springJoint)
        {
            Destroy(springJoint);
            SetLassoing(false);
            Debug.Log("Lasso detached");
        }
    }

    void UpdateLineRenderer()
    {
        if (springJoint.connectedBody == null)
        {
            DetachLasso();
            return;
        }
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, springJoint.connectedBody.transform.position);
    }
    
    IEnumerator ShowMissedShot(Vector3 clickPosition)
    {
        SetLassoing(true);
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

        // Only hide the line if the player hasn't successfully lassoed in the meantime
        if (!springJoint)
        {
            SetLassoing(false);
        }
    }
    
    private void SetLassoing(bool isLassoing)
    {
        this.isLassoing = isLassoing;
        lineRenderer.enabled = isLassoing;
        SetLassoSprite(isLassoing);
    }
}