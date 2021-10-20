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

    public PlayerIdleState idleState;
    public PlayerMoveLookState moveLookState;
    public PlayerFallingState fallingState;
    public PlayerFlyState flyState;


    private void Awake()
    {
        idleState = new PlayerIdleState(this);
        moveLookState = new PlayerMoveLookState(this);
        flyState = new PlayerFlyState(this);
        fallingState = new PlayerFallingState(this);

        cam = Camera.main;

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected override PlayerBaseState GetInitialState()
    {
        return idleState;
    }

    public void Landed()
    {
        if (currentState == fallingState)
        {
            anim.SetBool("Falling", false);
            Debug.Log("Landed");
            ChangeState(idleState);
        }
    }

}
