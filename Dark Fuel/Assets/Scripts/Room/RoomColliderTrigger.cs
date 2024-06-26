using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomColliderTrigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public void OnTriggerEnter()
    {
        OnEnter.Invoke();
    }

    public void OnTriggerExit()
    {
        OnExit.Invoke();
    }
}
