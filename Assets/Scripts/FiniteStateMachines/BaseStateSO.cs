using UnityEngine;

public enum EExecutionState : byte
{
    None,
    Active,
    Completed,
    Terminated
}

public abstract class BaseStateSO : ScriptableObject
{
    public EExecutionState ExecutionState {  get; protected set; }

    protected EnemyController ownerUnit;

    public virtual bool EnterState(EnemyController ownerUnit)
    {
        ExecutionState = EExecutionState.Active;
        if (!this.ownerUnit) this.ownerUnit = ownerUnit;

        return true;
    }

    public abstract void Tick();

    public virtual bool ExitState()
    {
        ExecutionState = EExecutionState.Completed;
        return true;
    }
}