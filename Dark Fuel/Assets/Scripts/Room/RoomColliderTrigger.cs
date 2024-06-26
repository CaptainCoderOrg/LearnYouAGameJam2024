using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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

    [Button("Resize Collider")]
    public void ResizeCollider()
    {
        MeshRenderer[] renderers = gameObject.transform.parent.GetComponentsInChildren<MeshRenderer>();
        Vector3 min = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        Vector3 max = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        foreach (MeshRenderer renderer in renderers)
        {
            min = Vector3.Min(min, renderer.bounds.min);
            max = Vector3.Max(max, renderer.bounds.max);
        }
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.size = max - min;
        transform.position = max - (collider.size / 2);
    }
}
