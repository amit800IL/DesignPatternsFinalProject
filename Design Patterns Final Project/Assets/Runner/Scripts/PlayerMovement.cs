using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    DesignPatternsFinalProject InputActions;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundDistance;
    Vector3 moveInput;
    Vector3 newMove;

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


}
