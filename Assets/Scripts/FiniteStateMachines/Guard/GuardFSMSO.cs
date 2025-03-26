using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/FSM/Guard")]
public class GuardFSMSO : BaseFSMSO
{
    public event EventHandler OnTimerTimeOut;

    public const float MAX_TIMER = 3.0f;
    private Dictionary<EState, BaseStateSO> stateDict = new Dictionary<EState, BaseStateSO>();
    public float Timer {  get; private set; }
    public int Failsafe { get; private set; }

    public override void Activate(EnemyController ownerUnit)
    {
        base.Activate(ownerUnit);

        stateDict[EState.Idle] = baseStateSOArray[0];
        stateDict[EState.Patrol] = baseStateSOArray[1];
        stateDict[EState.MoveTowardsPlayer] = baseStateSOArray[2];

        currentState = EState.Idle;
        currentStateSO = stateDict[currentState];

        Timer = 0.0f;
        Failsafe = 0;

        if (!currentStateSO.EnterState(ownerUnit))
        {
            Debug.LogError("Failed to enter default state!");
            return;
        }
    }

    public override void Tick(float deltaTime)
    {
        switch(currentState)
        {
            case EState.Idle:
                HandleIdle(deltaTime);
                break;
            case EState.Patrol:
                HandlePatrol(deltaTime);
                break;
            case EState.MoveTowardsPlayer:
                HandleMoveTowardsPlayer(deltaTime);
                break;
            default:
                break;
        }

        currentStateSO.Tick();
    }

    private void HandleIdle(float deltaTime)
    {
        // Transition to Move Towards Player
        if (ownerUnit.Target)
        {
            Timer = 0.0f;
            Failsafe = 0;
            OnTimerTimeOut?.Invoke(this, EventArgs.Empty);

            if (!ChangeState(EState.MoveTowardsPlayer))
            {
                Debug.LogError("Failed to transition into Move Towards Player!");
                return;
            }
            return;
        }

        // Transition to Patrol
        Timer += deltaTime;
        if (Timer > MAX_TIMER)
        {
            Timer = 0.0f;

            float rand = UnityEngine.Random.Range(0.0f, 100.0f);
            if (rand >= 50.0f || Failsafe >= 2)
            {
                Failsafe = 0;
                if (!ChangeState(EState.Patrol))
                {
                    Debug.LogError("Failed to transition into Patrol!");
                    return;
                }
            }
            else
            {
                Failsafe++;
            }

            OnTimerTimeOut?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandlePatrol(float deltaTime)
    {
        // Transition to Move Towards Player
        if (ownerUnit.Target)
        {
            Timer = 0.0f;
            Failsafe = 0;
            OnTimerTimeOut?.Invoke(this, EventArgs.Empty);

            if (!ChangeState(EState.MoveTowardsPlayer))
            {
                Debug.LogError("Failed to transition into Move Towards Player!");
                return;
            }
            return;
        }

        // Transition to Idle
        Timer += deltaTime;
        if (Timer > MAX_TIMER)
        {
            // If we still patrolling
            if (currentStateSO.ExecutionState == EExecutionState.Active)
            {
                Timer = MAX_TIMER;
                return;
            }

            Timer = 0.0f;

            float rand = UnityEngine.Random.Range(0.0f, 100.0f);
            if (rand >= 50.0f || Failsafe >= 2)
            {
                Failsafe = 0;
                if (!ChangeState(EState.Idle))
                {
                    Debug.LogError("Failed to transition into Idle!");
                    return;
                }
            }
            else
            {
                Failsafe++;
                if (!currentStateSO.EnterState(ownerUnit))
                {
                    Debug.LogError("Failed to enter Patrol state!");
                    return;
                }
            }

            OnTimerTimeOut?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleMoveTowardsPlayer(float deltaTime)
    {
        if (!ownerUnit.Target)
        {
            if (!ChangeState(EState.Idle))
            {
                Debug.LogError("Failed to transition into Idle!");
                return;
            }
        }
    }

    private bool ChangeState(EState state)
    {
        if (currentStateSO.ExitState())
        {
            currentState = state;
            currentStateSO = stateDict[currentState];
            if (!currentStateSO.EnterState(ownerUnit))
            {
                Debug.LogError($"Failed to enter {currentState} state!");
                return false;
            }
            OnStateChangedNotifyBegin();
        }
        else
        {
            Debug.LogError($"Failed to exit {currentState} state!");
            return false;
        }

        return true;
    }
}
