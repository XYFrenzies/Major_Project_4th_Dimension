using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovementSM : PlayerStateManager
{
    public Camera cam;
    public Animator animator;

    public PlayerInput playerInput;

    public InputAction moveAction;
    public InputAction lookAction;

    public Vector3 direction = Vector3.zero;
    public Vector2 inputs;
    public Vector2 lookInputs;
    public float moveSpeed = 10.0f;
    public Rigidbody rb;
    public Animator anim;
    public CinemachineVirtualCamera vCam;

    public PlayerIdleState idleState;
    public PlayerMoveLookState moveLookState;
    public PlayerFallingState fallingState;
    public PlayerFlyState flyState;
    public PlayerLandingState landingState;


    private void Awake()
    {
        idleState = new PlayerIdleState(this);
        moveLookState = new PlayerMoveLookState(this);
        flyState = new PlayerFlyState(this);
        fallingState = new PlayerFallingState(this);
        landingState = new PlayerLandingState(this);

        cam = Camera.main;

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        lookAction.Enable();
        moveAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
        moveAction.Disable();
    }

    protected override PlayerBaseState GetInitialState()
    {
        return moveLookState;
    }

    public void Landed()
    {
        anim.SetBool("Falling", false);
        Debug.Log("Landed");
        ChangeState(landingState);
    }

    public void OnControlsChanged()
    {
        Debug.Log("CONTROLS CHANGED");
    }

}
