using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/Patrol")]
public class PatrolStateSO : BaseStateSO
{
    private Vector3 pointA;
    private Vector3 pointB;
    private bool bIsReturning = false;

    public override bool EnterState(EnemyController ownerUnit)
    {
        base.EnterState(ownerUnit);

        Debug.Log("Entered Patrol State");

        Transform[] patrolPoints = ownerUnit.GetPatrolPoints();
        if (patrolPoints.Length < 1) return false;

        pointA = patrolPoints[0].transform.position;
        pointB = patrolPoints[1].transform.position;
        ownerUnit.NavMeshAgent.destination = bIsReturning ? pointA : pointB;

        return true;
    }

    public override void Tick()
    {
        if (!ownerUnit.NavMeshAgent.pathPending && ownerUnit.NavMeshAgent.remainingDistance <= ownerUnit.NavMeshAgent.stoppingDistance)
        {
            Debug.Log("Reach!");
            bIsReturning = !bIsReturning;
            ownerUnit.NavMeshAgent.destination = bIsReturning ? pointA : pointB;
        }
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exitting Patrol State");

        return true;
    }
}
