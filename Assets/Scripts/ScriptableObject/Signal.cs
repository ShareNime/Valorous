using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu]
class Signal : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].onSignalRaised();
        }
    }
    public void RegisterListerner(SignalListener listener)
    {
        listeners.Add(listener);
    }
    public void DeRegisterListerner(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
