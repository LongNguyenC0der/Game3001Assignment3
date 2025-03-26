using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/MoveTowardsPlayer")]
public class MoveTowardsPlayerStateSO : BaseStateSO
{
    public override bool EnterState(EnemyController ownerUnit)
    {
        base.EnterState(ownerUnit);

        Debug.Log("Entered MoveTowardsPlayer State");
        ownerUnit.NavMeshAgent.isStopped = false;
        ownerUnit.IsAggressive = true;

        return true;
    }

    public override void Tick()
    {
        ownerUnit.NavMeshAgent.destination = ownerUnit.Target.position;
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting MoveTowardsPlayer State");
        ownerUnit.NavMeshAgent.isStopped = true;
        ownerUnit.IsAggressive = false;

        return true;
    }
}
