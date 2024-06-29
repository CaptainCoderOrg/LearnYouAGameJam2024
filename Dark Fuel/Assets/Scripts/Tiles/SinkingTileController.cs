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
    public HashSet<GameObject> OnTop = new();
    public Vector3 Size = new (5, 0, 5);
    public float CheckDistance = 0.05f;
    public Transform GroundCheckPosition;

    public void Awake()
    {
        OriginalHeight = transform.position.y;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.IsAbove())
        {
            OnTop.Add(collision.gameObject);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        OnTop.Remove(collision.gameObject);
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
        foreach (GameObject gameObject in OnTop)
        {
            position = gameObject.transform.position;
            position.y -= SinkingSpeed * Time.fixedDeltaTime;
            gameObject.transform.position = position;
        }
    }
}
