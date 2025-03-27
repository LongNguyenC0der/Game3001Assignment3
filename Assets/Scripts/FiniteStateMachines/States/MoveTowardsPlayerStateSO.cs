using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/MoveTowardsPlayer")]
public class MoveTowardsPlayerStateSO : BaseStateSO
{
    public override bool EnterState(EnemyController ownerUnit)
    {
        base.EnterState(ownerUnit);

        Debug.Log("Entered MoveTowardsPlayer State");
        ownerUnit.IsAggressive = true;

        return true;
    }

    public override void Tick()
    {
        if (ownerUnit.Target)
        {
            ownerUnit.NavMeshAgent.destination = ownerUnit.Target.position;
        }
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting MoveTowardsPlayer State");
        ownerUnit.IsAggressive = false;

        return true;
    }
}
