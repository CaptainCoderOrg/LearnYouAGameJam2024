using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour
{
    public UnityEvent<Collider> OnTriggerEntered;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        OnTriggerEntered.Invoke(other);
    }
}
