using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanController : MonoBehaviour
{
    public LevelController LevelController;
    public void Awake()
    {
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
        LevelController.BeansCollected++;
        Destroy(gameObject);
    }
}
