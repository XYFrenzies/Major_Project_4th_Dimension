using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyState : PlayerBaseState
{
    public PlayerFlyState(PlayerStateManager psm) : base(psm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered fly state");
    }

    public override void ExitState()
    {

    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {

    }
}
