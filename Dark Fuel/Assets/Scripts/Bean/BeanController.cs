using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanController : MonoBehaviour
{
    
    public void OnTriggerEntered(Collider other)
    {
        if (other.attachedRigidbody.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}
