using System;
using UnityEngine;

public enum EState : byte
{
    Idle,
    Patrol,
    MoveTowardsPlayer
}

public abstract class BaseFSMSO : ScriptableObject
{
    public class OnStateChangedEventArgs : EventArgs
    {
        public EState state;
    }
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

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
    protected void OnStateChangedNotifyBegin()
    {
        SoundManager.Instance.PlayEffectSound();
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });
    }
}
