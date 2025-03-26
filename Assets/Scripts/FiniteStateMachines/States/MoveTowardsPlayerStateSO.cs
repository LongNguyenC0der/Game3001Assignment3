using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/MoveTowardsPlayer")]
public class MoveTowardsPlayerStateSO : BaseStateSO
{
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 currentDestination;
    private bool bIsReturning = false;

    public override bool EnterState(EnemyController ownerUnit)
    {
        base.EnterState(ownerUnit);

        Debug.Log("Entered Patrol State");
        bIsReturning = false;

        Transform[] patrolPoints = ownerUnit.GetPatrolPoints();
        if (patrolPoints.Length < 1) return false;

        pointA = patrolPoints[0].transform.position;
        pointB = patrolPoints[1].transform.position;
        ownerUnit.NavMeshAgent.destination = bIsReturning ? pointA : pointB;

        return true;
    }

    public override void Tick()
    {
        if (ExecutionState == EExecutionState.Active)
        {
            if (!ownerUnit.NavMeshAgent.pathPending && ownerUnit.NavMeshAgent.remainingDistance <= ownerUnit.NavMeshAgent.stoppingDistance)
            {
                Debug.Log("Reach!");

                if (bIsReturning && currentDestination == pointA)
                {
                    ExitState();
                    return;
                }

                bIsReturning = !bIsReturning;
                currentDestination = bIsReturning ? pointA : pointB;
                ownerUnit.NavMeshAgent.destination = currentDestination;
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exitting Patrol State");

        return true;
    }
}
