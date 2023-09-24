using UnityEngine;
using UnityEngine.Events;
class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;
    public void onSignalRaised()
    {
        signalEvent.Invoke();
    }
    private void OnEnable()
    {
        signal.RegisterListerner(this);
    }
    private void OnDisable()
    {
        signal.DeRegisterListerner(this);
    }
}
