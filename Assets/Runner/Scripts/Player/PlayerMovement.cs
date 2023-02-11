using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject obstacle;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundDistance;
    [SerializeField] float speed;
    private Rigidbody rb;
    private DesignPatternsFinalProject InputActions;
    private Vector3 newMove;
    private Vector3 moveInput;

    private bool isGrounded;

    public void IsGrounded() => isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        InputActions = new();
        InputActions.Enable();
    }

    private void Update()
    {
        IsGrounded();

        newMove = moveInput.x * transform.right + moveInput.y * transform.forward;

        if (newMove.magnitude > 1)
        {
            newMove.Normalize();
        }



    }

    private void FixedUpdate()
    {
        if (newMove != null)
        {
            rb.AddForce(newMove * speed, ForceMode.Impulse);
        }
    }

    private void OnMove(InputValue value)
    {
        newMove = value.Get<Vector2>();

        newMove = new Vector3(-newMove.x, 0, -newMove.y);

        rb.velocity = newMove * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.OnPlayerDeath();
        }
    }


}
