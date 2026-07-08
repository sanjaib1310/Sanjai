using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jetpackThrust = 15f;
    [SerializeField] private float groundDrag = 5f;

    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 moveDirection;
    private float currentSpeed;
    private bool isGrounded = true;
    private bool isJetpackActive = false;
    private float jetpackFuel = 100f;
    private float maxJetpackFuel = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        HandleInput();
        HandleJetpack();
    }

    private void FixedUpdate()
    {
        Move();
        SpeedControl();
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ActivateJetpack();
        }
    }

    private void Move()
    {
        rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void HandleJetpack()
    {
        if (isJetpackActive && jetpackFuel > 0)
        {
            rb.AddForce(Vector3.up * jetpackThrust, ForceMode.Acceleration);
            jetpackFuel -= Time.deltaTime * 20f;
        }
        else
        {
            isJetpackActive = false;
        }

        if (!isJetpackActive && jetpackFuel < maxJetpackFuel)
        {
            jetpackFuel = Mathf.Min(jetpackFuel + Time.deltaTime * 10f, maxJetpackFuel);
        }
    }

    private void ActivateJetpack()
    {
        if (jetpackFuel > 0)
        {
            isJetpackActive = true;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public float GetJetpackFuelPercentage() => (jetpackFuel / maxJetpackFuel) * 100f;
}
