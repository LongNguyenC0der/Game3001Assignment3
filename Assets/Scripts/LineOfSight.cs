using System;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public class OnPlayerVisibleEventArgs : EventArgs
    {
        public Transform target;
    }

    public event EventHandler<OnPlayerVisibleEventArgs> OnPlayerVisible;
    public event EventHandler OnPlayerOutOfSight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            OnPlayerVisible?.Invoke(this, new OnPlayerVisibleEventArgs { target = player.transform });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnPlayerOutOfSight?.Invoke(this, EventArgs.Empty);
    }
}
