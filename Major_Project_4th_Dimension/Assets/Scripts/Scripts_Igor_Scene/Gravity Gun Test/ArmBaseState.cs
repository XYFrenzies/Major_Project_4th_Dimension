using UnityEngine;

public abstract class ArmBaseState : MonoBehaviour
{
    public abstract void AwakeState(ArmStateManager arm);

    public abstract void EnterState(ArmStateManager arm);

    public abstract void UpdateState(ArmStateManager arm);

    public abstract void OnEnableState(ArmStateManager arm);

    public abstract void OnDisableState(ArmStateManager arm);

}
