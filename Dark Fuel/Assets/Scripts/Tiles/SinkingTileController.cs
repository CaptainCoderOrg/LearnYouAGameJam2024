using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingTileController : MonoBehaviour
{
    public LayerMask Layers;
    public float OriginalHeight = 0;
    public float SinkingSpeed = 5f;
    public bool IsSinking => OnTop.Count > 0;
    public HashSet<AttachToTileController> OnTop = new();
    public Vector3 Size = new (5, 0, 5);
    public float CheckDistance = 0.05f;
    public Transform GroundCheckPosition;

    public void Awake()
    {
        OriginalHeight = transform.position.y;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<AttachToTileController>(out var controller) && collision.IsAbove())
        {
            controller.Detach = () => Detach(controller);
            OnTop.Add(controller);
        }
    }

    private void Attach(AttachToTileController controller)
    {
        if (OnTop.Add(controller))
        {
            controller.Detach = () => Detach(controller);
            controller.AttachedTo = gameObject;
        }
    }

    private void Detach(AttachToTileController controller)
    {
        if (OnTop.Remove(controller))
        {
            controller.AttachedTo = null;
            controller.Detach = null;
        }        
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<AttachToTileController>(out var controller))
        {
            OnTop.Remove(controller);
        }
    }

    public void FixedUpdate()
    {
        if (IsSinking) { Sink(); }
        else { Float(); }
    }

    public void Float()
    {
        Vector3 position = transform.position;
        position.y += SinkingSpeed * Time.fixedDeltaTime;
        position.y = Math.Min(OriginalHeight, position.y);
        transform.position = position;
    }

    public void Sink()
    {
        float maxDistance = SinkingSpeed * Time.fixedDeltaTime;
        if (Physics.BoxCast(GroundCheckPosition.position, Size / 2, Vector3.down, out RaycastHit hitInfo, Quaternion.identity, CheckDistance, Layers))
        {
            Debug.Log(hitInfo.distance);
            maxDistance = Math.Min(hitInfo.distance, maxDistance);
        }
        if (maxDistance <= 0.0001) { return; }
        Vector3 position = transform.position;
        position.y -= maxDistance;
        transform.position = position;
        foreach (AttachToTileController attached in OnTop)
        {
            position = attached.transform.position;
            position.y -= SinkingSpeed * Time.fixedDeltaTime;
            attached.transform.position = position;
        }
    }
}
