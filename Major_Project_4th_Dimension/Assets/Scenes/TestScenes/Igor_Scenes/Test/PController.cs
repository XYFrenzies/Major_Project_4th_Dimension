using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PController : MonoBehaviour
{
    private PlayerInput playerInput;

    public float speed = 5f;
    //public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public float playerRotationSpeed = 2f;
    public LayerMask ground;

    private Transform cameraTransform;
    private Rigidbody rb;
    private Vector2 inputs;
    private Vector3 direction = Vector3.zero;
    private bool isGrounded = true;
    private Transform groundChecker;

    private InputAction moveAction;



    void Awake()
    {
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        groundChecker = transform.GetChild(0);
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];

    }

    void Update()
    {
        PlayerMove();
        PlayerLook();

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void PlayerMove()
    {
        direction = Vector3.zero;
        inputs = moveAction.ReadValue<Vector2>();
        direction.x = inputs.x;
        direction.z = inputs.y;
        if (direction != Vector3.zero)
            //transform.forward = direction;
            direction = direction.x * cameraTransform.right.normalized + direction.z * cameraTransform.forward.normalized;
        direction.y = 0f;
    }

    public void PlayerLook()
    {
        //Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    public bool GroundedCheck()
    {
        return isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
    }
}