using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {

    }

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.EnterState();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    public void ChangeState(PlayerBaseState nextState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = nextState;

        if (currentState != null)
            currentState.EnterState();
    }

    protected virtual PlayerBaseState GetInitialState()
    {
        return null;
    }
}
