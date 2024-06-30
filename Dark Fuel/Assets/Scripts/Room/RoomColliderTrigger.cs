using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class RoomColliderTrigger : MonoBehaviour
{
    public LayerMask WallLayer;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public void OnTriggerEnter()
    {
        OnEnter?.Invoke();
    }

    public void OnTriggerExit()
    {
        OnExit?.Invoke();
    }

    private Vector3 _min = Vector3.zero;
    private Vector3 _max = Vector3.zero;
    [Button("Add Invisible Walls")]
    public void AddInvisibleWalls()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        CalculateMinMax();
        float bottomY = _max.y;
        float height = 10;
        GameObject west = new ("west");
        west.transform.parent = transform;
        west.transform.position = new Vector3(_max.x - (_max.x - _min.x) / 2, bottomY, _max.z);
        BoxCollider westCollider = west.AddComponent<BoxCollider>();
        westCollider.center = new Vector3(0, height/2, 0);
        westCollider.size = new Vector3(_max.x - _min.x, height, 0);
        westCollider.includeLayers = WallLayer;
        GameObject east = new ("east");
        east.transform.position = new Vector3(_max.x, bottomY, _max.z - (_max.z - _min.z) / 2);
        east.transform.parent = transform;
        BoxCollider eastCollider = east.AddComponent<BoxCollider>();
        eastCollider.center = new Vector3(0, height/2, 0);
        eastCollider.size = new Vector3(0, height, _max.z - _min.z);
        eastCollider.includeLayers = WallLayer;
        GameObject north = new ("north");
        north.transform.position = new Vector3(_max.x - (_max.x - _min.x) / 2, bottomY, _min.z);
        north.transform.parent = transform;
        BoxCollider northCollider = north.AddComponent<BoxCollider>();
        northCollider.center = new Vector3(0, height/2, 0);
        northCollider.size = new Vector3(_max.x - _min.x, height, 0);
        northCollider.includeLayers = WallLayer;
        GameObject south = new ("south");
        south.transform.position = new Vector3(_min.x, bottomY, _max.z - (_max.z - _min.z) / 2);
        south.transform.parent = transform;
        BoxCollider southCollider = south.AddComponent<BoxCollider>();
        southCollider.center = new Vector3(0, height/2, 0);
        southCollider.size = new Vector3(0, height, _max.z - _min.z);
        southCollider.includeLayers = WallLayer;
    }

    private void CalculateMinMax()
    {
        if (_min == Vector3.zero)
        {
            MeshRenderer[] renderers = gameObject.transform.parent.GetComponentsInChildren<MeshRenderer>();
            _min = new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            _max = new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            foreach (MeshRenderer renderer in renderers)
            {
                _min = Vector3.Min(_min, renderer.bounds.min);
                _max = Vector3.Max(_max, renderer.bounds.max);
            }
        }
    }

    [Button("Resize Collider")]
    public void ResizeCollider()
    {
        CalculateMinMax();
        BoxCollider collider = GetComponent<BoxCollider>();
        _max.y += 10;
        collider.size = _max - _min;
        transform.position = _max - (collider.size / 2);
    }
}
