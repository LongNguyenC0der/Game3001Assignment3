using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private BaseFSMSO finiteStateMachine;
    public NavMeshAgent NavMeshAgent { get; private set; }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
