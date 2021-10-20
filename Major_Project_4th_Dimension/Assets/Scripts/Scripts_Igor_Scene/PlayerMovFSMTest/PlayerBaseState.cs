
public class PlayerBaseState
{ 
    protected PlayerStateManager PSManager;

    public PlayerBaseState(PlayerStateManager psm)
    {
        PSManager = psm;
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void UpdateLogic() { }

    public virtual void UpdatePhysics() { }
}
