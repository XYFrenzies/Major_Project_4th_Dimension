
public abstract class PlayerBaseState
{ 
    protected PlayerStateManager PSManager;

    public PlayerBaseState(PlayerStateManager psm)
    {
        PSManager = psm;
    }

    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void UpdateLogic();

    public abstract void UpdatePhysics();
}
