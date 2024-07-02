using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public string Name = "Unnamed Room";
    public MeshRenderer[] Renderers;
    public SkinnedMeshRenderer[] SkinnedRenderers;
    [field: SerializeField]
    public RoomColliderTrigger RoomCollider { get; private set;}
    [field: SerializeField]
    public CameraFocus CameraFocus { get; private set; }
    
    void Awake()
    {
        CameraFocus = GetComponentInChildren<CameraFocus>();
        Debug.Assert(CameraFocus != null);
        
        Renderers = GetComponentsInChildren<MeshRenderer>();
        Debug.Assert(Renderers.Length > 0);
        SkinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        Hide();

        RoomCollider = GetComponentInChildren<RoomColliderTrigger>();
        Debug.Assert(RoomCollider != null);

        RoomCollider.OnEnter.AddListener(OnEnter);
        RoomCollider.OnExit.AddListener(OnExit);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button("Show")]
    public void Show()
    {
        foreach (var renderer in Renderers)
        {
            if (renderer == null) { continue; }
            renderer.enabled = true;
        }
        foreach (var renderer in SkinnedRenderers)
        {
            if (renderer == null) { continue; }
            renderer.enabled = true;
        }
        RoomCollider.AddInvisibleWalls();
    }

    [Button("Hide")]
    public void Hide()
    {
        foreach (var renderer in Renderers)
        {
            if (renderer == null) { continue; }
            renderer.enabled = false;
        }
        foreach (var renderer in SkinnedRenderers)
        {
            if (renderer == null) { continue; }
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
