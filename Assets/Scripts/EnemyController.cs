using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private BaseFSMSO finiteStateMachine;
    [SerializeField] private Transform[] patrolPoints;
    public NavMeshAgent NavMeshAgent { get; private set; }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
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
            finiteStateMachine.Activate(this);
        }
    }

    private void Update()
    {
        finiteStateMachine.Tick(Time.deltaTime);
    }

    public Transform[] GetPatrolPoints() { return patrolPoints; }
    public BaseFSMSO GetBaseFSMSO() { return finiteStateMachine; }
}
