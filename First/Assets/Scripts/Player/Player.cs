using UnityEngine;

public class Player : MonoBehaviour
{
    public Collider col;
    public Vector3 hVelocity;
    public Vector3 vVelocity;
    public ProjectileHandler ph;
    public Rigidbody rb;
    public float gConstant = 9.8f;
    public float hSpeed = 1;
    public float forceAmount;
    public float jumpAmount = 10f;
    public bool isJumping = false;

    private Vector3 gravity;
    private Vector3 verticalColliderExtents;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        verticalColliderExtents = new Vector3(0, col.bounds.extents.y, 0);
        rb = GetComponent<Rigidbody>();
        ph = GetComponent<ProjectileHandler>();
        hVelocity = new Vector3(1 * hSpeed, 0, 0);
        gravity = Vector3.down * gConstant;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        PlayerProjectile();
        PlayerMovement();

        RaycastHit hit;
        Vector3 rayOrigin = transform.position - verticalColliderExtents;
        if (Physics.Raycast(rayOrigin, gravity, out hit))
        {
            Debug.DrawRay(rayOrigin, gravity.normalized * hit.distance, Color.red);

            if (hit.distance < 0.1f)
            {
                gravity = Vector3.zero;
            }
        }

        transform.Translate((hVelocity + gravity) * Time.deltaTime);
    }

    void PlayerProjectile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ph.Shoot();
        }
    }

    void ApplyGravity()
    {
        gravity = Vector3.down * gConstant;
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            hVelocity = new Vector3(1 * hSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hVelocity = new Vector3(-1 * hSpeed, 0, 0);
        }
        else
        {
            hVelocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = Vector3.up * jumpAmount;
        }
    }

    void PlayerCollision()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
