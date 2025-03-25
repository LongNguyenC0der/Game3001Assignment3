using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/Idle")]
public class IdleStateSO : BaseStateSO
{
    public override bool EnterState(EnemyController ownerUnit)
    {
        base.EnterState(ownerUnit);

        Debug.Log("Entered Idle State");

        return true;
    }

    public override void Tick()
    {
        Debug.Log("Idle State is Ticking!");
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting Idle State");

        return true;
    }
}
