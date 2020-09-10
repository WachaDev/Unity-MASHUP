using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float radius = 0.3f;
    [SerializeField] private bool isGrounded;

    void Start()
    {
        input = GetComponent<InputManager>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        groundMask = LayerMask.GetMask("Ground");
        groundChecker = GetComponent<Transform>().GetChild(1);
    }

    void FixedUpdate()
    {
        Controller();
    }

    private void Controller() 
    {
        Vector3 move = transform.right * input.horizontal + transform.forward * input.vertical;
        rb.AddForce(move * speed * Time.deltaTime, ForceMode.VelocityChange); 
        
        isGrounded = Physics.CheckSphere(groundChecker.position, radius, groundMask);
        if (isGrounded && input.jump)
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
    }
}
