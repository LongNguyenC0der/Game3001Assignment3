using UnityEngine;

public class BaseFSMSO : MonoBehaviour
{
    BaseStateSO startingState;
    BaseStateSO currentState;

    private void Awake()
    {
        currentState = null;
    }

    private void Start()
    {
        if (startingState == null)
        {
            EnterState(startingState);
        }
    }

    private void Update()
    {
        if (currentState)
        {
            currentState.Tick();
        }
    }

    private void EnterState(BaseStateSO nextState)
    {
        if (!nextState) return;

        currentState = nextState;
        currentState.EnterState();
    }
}
