using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private BaseFSMSO finiteStateMachine;
    [SerializeField] private Transform[] patrolPoints;
    public NavMeshAgent NavMeshAgent { get; private set; }
    public LineOfSight LineOfSight { get; private set; } // This might not need to be a property
    public Transform Target { get; private set; } = null;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        LineOfSight = GetComponentInChildren<LineOfSight>();
    }

    private void Start()
    {
        if (!finiteStateMachine)
        {
            Debug.LogError("EnemyController doesn't have an FSM!");
            return;
        }
        else
        {
            LineOfSight.OnPlayerVisible += LineOfSight_OnPlayerVisible;
            LineOfSight.OnPlayerOutOfSight += LineOfSight_OnPlayerOutOfSight;

            finiteStateMachine.Activate(this);
        }
    }

    private void LineOfSight_OnPlayerVisible(object sender, LineOfSight.OnPlayerVisibleEventArgs e)
    {
        Target = e.target;
    }

    private void LineOfSight_OnPlayerOutOfSight(object sender, System.EventArgs e)
    {
        Target = null;
    }

    private void Update()
    {
        finiteStateMachine.Tick(Time.deltaTime);
    }

    public Transform[] GetPatrolPoints() { return patrolPoints; }
    public BaseFSMSO GetBaseFSMSO() { return finiteStateMachine; }
}
