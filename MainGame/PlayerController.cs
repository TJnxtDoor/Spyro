using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float horizontal  = Input.GetAxis("horizontal");
        float vertical = Input.GetAxis("vertical");
        Vector3 movement = new Vector3(horizontal, 0 , vertical) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    // Handle enemy collision (e.g., reduce health, respawn, etc.)
                }
        }
    }
}