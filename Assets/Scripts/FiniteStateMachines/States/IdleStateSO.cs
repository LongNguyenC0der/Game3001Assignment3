using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/States/Idle")]
public class IdleStateSO : BaseStateSO
{
    public override bool EnterState()
    {
        base.EnterState();

        Debug.Log("Entered IDLE State");

        return true;
    }

    public override void Tick()
    {
        Debug.Log("Idle State is Ticking!");
    }

    public override bool ExitState()
    {
        base.ExitState();

        Debug.Log("Exitting IDLE State");

        return true;
    }
}
