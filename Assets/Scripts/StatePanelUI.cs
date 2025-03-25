using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatePanelUI : MonoBehaviour
{
    [SerializeField] private Toggle idleToggle;
    [SerializeField] private Toggle patrolToggle;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text failsafeText;

    private GuardFSMSO fsm;

    private void Start()
    {
        fsm = FindFirstObjectByType<EnemyController>().GetBaseFSMSO() as GuardFSMSO;
        fsm.OnStateChanged += BaseFSMSO_OnStateChanged;
        fsm.OnTimerTimeOut += GuardFSMSO_OnTimerTimeOut;
        idleToggle.isOn = true;
        failsafeText.text = $"Failsafe: 0";
    }

    private void OnDestroy()
    {
        if (fsm)
        {
            fsm.OnStateChanged -= BaseFSMSO_OnStateChanged;
            fsm.OnTimerTimeOut -= GuardFSMSO_OnTimerTimeOut;
        }
    }

    private void Update()
    {
        timerText.text = $"Timer: {(GuardFSMSO.MAX_TIMER - fsm.Timer):F2}s";
    }

    private void BaseFSMSO_OnStateChanged(object sender, BaseFSMSO.OnStateChangedEventArgs e)
    {
        switch (e.state)
        {
            case EState.Idle:
                idleToggle.isOn = true;
                break;
            case EState.Patrol:
                patrolToggle.isOn = true;
                break;
            case EState.MoveTowardsPlayer:
                break;
            default:
                break;
        }
    }

    private void GuardFSMSO_OnTimerTimeOut(object sender, System.EventArgs e)
    {
        failsafeText.text = $"Failsafe: {fsm.Failsafe}";
    }
}
