using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollisionController : MonoBehaviour
{
    public UnityEvent OnPressed;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public float NormalThreshold = 0.1f;
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y <= (-1.0 + NormalThreshold))
            {
                OnPressed.Invoke();
            }
        }
    }

    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.y = (100 - SkinnedMeshRenderer.GetBlendShapeWeight(0)) / 100;
        transform.localScale = scale;
    }
}