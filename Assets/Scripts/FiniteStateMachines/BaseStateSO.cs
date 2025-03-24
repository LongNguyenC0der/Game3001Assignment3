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

    public virtual void OnEnable()
    {
        ExecutionState = EExecutionState.None;
    }

    public virtual bool EnterState()
    {
        ExecutionState = EExecutionState.Active;
        return true;
    }

    public abstract void Tick();

    public virtual bool ExitState()
    {
        ExecutionState = EExecutionState.Completed;
        return true;
    }
}