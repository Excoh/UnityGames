using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float forceAmount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
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
    }
}
