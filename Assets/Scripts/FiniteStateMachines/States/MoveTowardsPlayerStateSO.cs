using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/MoveTowardsPlayer")]
public class MoveTowardsPlayerStateSO : BaseStateSO
{
    private Transform target;

    public override bool EnterState(EnemyController ownerUnit)
    {
        base.EnterState(ownerUnit);

        Debug.Log("Entered MoveTowardsPlayer State");

        return true;
    }

    public override void Tick()
    {
        //if (ExecutionState == EExecutionState.Active)
        //{
        //    if (!ownerUnit.NavMeshAgent.pathPending && ownerUnit.NavMeshAgent.remainingDistance <= ownerUnit.NavMeshAgent.stoppingDistance)
        //    {
        //        if (bIsReturning && currentDestination == pointA)
        //        {
        //            ExitState();
        //            return;
        //        }

        //        bIsReturning = !bIsReturning;
        //        currentDestination = bIsReturning ? pointA : pointB;
        //        ownerUnit.NavMeshAgent.destination = currentDestination;
        //    }
        //}
        Debug.Log("MoveTowardsPlayer is ticking!");
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting MoveTowardsPlayer State");

        return true;
    }
}
