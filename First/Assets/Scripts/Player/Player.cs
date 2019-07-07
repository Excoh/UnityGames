using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public ProjectileHandler ph;
    public Rigidbody rb;
    public float forceAmount;
    public float jumpAmount;
    public bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ph = GetComponent<ProjectileHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerProjectile();
        PlayerMovement();
    }

    void PlayerProjectile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ph.Shoot();
        }
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * forceAmount);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position - transform.right * Time.deltaTime * forceAmount);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpAmount);
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
