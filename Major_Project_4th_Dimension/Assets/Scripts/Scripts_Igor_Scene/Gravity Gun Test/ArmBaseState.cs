using UnityEngine;

public abstract class ArmBaseState
{

    protected ArmStateManager armStateMan;

    public ArmBaseState(ArmStateManager arm)
    {
        armStateMan = arm;
    }

    

    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void UpdateState();


}
