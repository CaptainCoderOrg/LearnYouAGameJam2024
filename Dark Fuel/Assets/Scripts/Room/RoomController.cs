using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public string Name = "Unnamed Room";
    public MeshRenderer[] Renderers;
    [field: SerializeField]
    public RoomColliderTrigger RoomCollider { get; private set;}
    [field: SerializeField]
    public CameraFocus CameraFocus { get; private set; }
    void Awake()
    {
        CameraFocus = GetComponentInChildren<CameraFocus>();
        Debug.Assert(CameraFocus != null);
        RoomCollider = GetComponentInChildren<RoomColliderTrigger>();
        Debug.Assert(RoomCollider != null);
        RoomCollider.OnEnter.AddListener(OnEnter);
        RoomCollider.OnExit.AddListener(OnExit);
        Renderers = GetComponentsInChildren<MeshRenderer>();
        Debug.Assert(Renderers.Length > 0);
        foreach (var renderer in Renderers)
        {
            renderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        foreach (var renderer in Renderers)
        {
            renderer.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (var renderer in Renderers)
        {
            renderer.enabled = false;
        }
    }

    public void OnEnter()
    {
        Debug.Log($"Entered: {Name}");
        CameraFocus.CameraFollower.QueueRoom(this);
    }

    public void OnExit()
    {
        Debug.Log($"Exited: {Name}");
        CameraFocus.CameraFollower.LeaveRoom(this);
    }
}
