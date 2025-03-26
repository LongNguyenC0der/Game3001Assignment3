using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private BaseFSMSO finiteStateMachine;
    [SerializeField] private Transform[] patrolPoints;
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Transform Target { get; private set; } = null;
    public bool IsAggressive { get; set; } = false;

    private LineOfSight lineOfSight;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        lineOfSight = GetComponentInChildren<LineOfSight>();
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
            lineOfSight.OnPlayerVisible += LineOfSight_OnPlayerVisible;
            lineOfSight.OnPlayerOutOfSight += LineOfSight_OnPlayerOutOfSight;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            if (IsAggressive)
            {
                print("We lose!");
            }
            else
            {
                print("We win!");
            }
        }
    }

    public Transform[] GetPatrolPoints() { return patrolPoints; }
    public BaseFSMSO GetBaseFSMSO() { return finiteStateMachine; }
}
