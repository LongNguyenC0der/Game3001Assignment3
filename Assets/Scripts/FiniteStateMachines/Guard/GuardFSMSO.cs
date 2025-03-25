using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Scriptable Objects/FSM/Guard")]
public class GuardFSMSO : BaseFSMSO
{
    private const float MAX_TIMER = 3.0f;
    private Dictionary<EState, BaseStateSO> stateDict = new Dictionary<EState, BaseStateSO>();
    public float Timer {  get; private set; }

    public override void Activate(EnemyController ownerUnit)
    {
        base.Activate(ownerUnit);

        stateDict[EState.Idle] = baseStateSOArray[0];
        stateDict[EState.Patrol] = baseStateSOArray[1];

        currentState = EState.Idle;
        currentStateSO = stateDict[currentState];

        Timer = 0.0f;

        if (!currentStateSO.EnterState(ownerUnit))
        {
            Debug.LogError("Failed to enter default state!");
            return;
        }
    }

    public override void Tick(float deltaTime)
    {
        if (currentStateSO.ExecutionState != EExecutionState.Active)
        {
            Debug.LogError($"State failed with status: {currentStateSO.ExecutionState}");
            return;
        }

        switch(currentState)
        {
            case EState.Idle:
                // Consider changing states after x amount of time
                Timer += deltaTime;
                if (Timer > MAX_TIMER)
                {
                    Timer = 0.0f;
                    if (currentStateSO.ExitState())
                    {
                        currentState = EState.Patrol;
                        currentStateSO = stateDict[currentState];
                        if (!currentStateSO.EnterState(ownerUnit))
                        {
                            Debug.LogError("Failed to enter Patrol state!");
                            return;
                        }
                    }
                }
                break;
            case EState.Patrol:
                // Consider changing states after x amount of time
                Timer += deltaTime;
                if (Timer > MAX_TIMER)
                {
                    Timer = 0.0f;
                    if (currentStateSO.ExitState())
                    {
                        currentState = EState.Idle;
                        currentStateSO = stateDict[currentState];
                        if (!currentStateSO.EnterState(ownerUnit))
                        {
                            Debug.LogError("Failed to enter Idle state!");
                            return;
                        }
                    }
                }
                break;
            case EState.MoveTowardsPlayer:
                break;
            default:
                break;
        }

        currentStateSO.Tick();
    }
}
