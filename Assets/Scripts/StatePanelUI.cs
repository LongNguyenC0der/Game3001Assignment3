using UnityEngine;
using UnityEngine.UI;

public class StatePanelUI : MonoBehaviour
{
    [SerializeField] private Toggle idleToggle;
    [SerializeField] private Toggle patrolToggle;

    private void Start()
    {
        FindFirstObjectByType<EnemyController>().GetBaseFSMSO().OnStateChanged += BaseFSMSO_OnStateChanged;
    }

    private void OnDestroy()
    {
        EnemyController enemy = FindFirstObjectByType<EnemyController>();
        if (enemy)
        {
            enemy.GetBaseFSMSO().OnStateChanged -= BaseFSMSO_OnStateChanged;
        }
    }

    private void BaseFSMSO_OnStateChanged(object sender, BaseFSMSO.OnStateChangedEventArgs e)
    {
        print($"PANEL: {e.state}");
    }
}
