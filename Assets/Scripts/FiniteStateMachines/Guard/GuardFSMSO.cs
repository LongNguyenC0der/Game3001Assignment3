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
        //if (currentStateSO.ExecutionState != EExecutionState.Active)
        //{
        //    Debug.LogError($"State failed with status: {currentStateSO.ExecutionState}");
        //    return;
        //}

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
        Timer += deltaTime;
        if (Timer > MAX_TIMER)
        {
            Timer = 0.0f;

            float rand = UnityEngine.Random.Range(0.0f, 100.0f);
            if (rand >= 50.0f || Failsafe >= 2)
            {
                Failsafe = 0;
                if (currentStateSO.ExitState())
                {
                    currentState = EState.Patrol;
                    currentStateSO = stateDict[currentState];
                    if (!currentStateSO.EnterState(ownerUnit))
                    {
                        Debug.LogError("Failed to enter Patrol state!");
                        return;
                    }
                    OnStateChangedNotifyBegin();
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
                if (currentStateSO.ExitState())
                {
                    currentState = EState.Idle;
                    currentStateSO = stateDict[currentState];
                    if (!currentStateSO.EnterState(ownerUnit))
                    {
                        Debug.LogError("Failed to enter Idle state!");
                        return;
                    }
                    OnStateChangedNotifyBegin();
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
        // If the player is in line of sight
    }
}
