using UnityEngine;

public enum EState : byte
{
    Idle,
    Patrol,
    MoveTowardsPlayer
}

public abstract class BaseFSMSO : ScriptableObject
{
    [SerializeField] protected BaseStateSO[] baseStateSOArray;
    protected BaseStateSO currentStateSO;
    protected EState currentState;

    protected EnemyController ownerUnit;

    public virtual void Activate(EnemyController ownerUnit)
    {
        if (baseStateSOArray.Length == 0)
        {
            Debug.LogError("Finite State Machine does not contain any states!");
            return;
        }

        this.ownerUnit = ownerUnit;
    }

    public abstract void Tick(float deltaTime);
}
