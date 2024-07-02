using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanController : MonoBehaviour
{
    public LevelController LevelController;
    public Animator Animator;
    bool _collected = false;
    public void Awake()
    {
        Animator = GetComponent<Animator>();
        LevelController = FindFirstObjectByType<LevelController>();
        LevelController.TotalBeans++;
    }
    
    public void OnTriggerEntered(Collider other)
    {
        if (other.attachedRigidbody.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (_collected) { return; }
        _collected = true;
        LevelController.BeansCollected++;
        Animator.SetTrigger("Collect");
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
