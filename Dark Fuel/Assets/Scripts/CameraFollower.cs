using System.Collections;
using System.Collections.Generic;
using CaptainCoder.UnityEngine;
using UnityEngine;

namespace CaptainCoder.DarkFuel
{
    public class CameraFollower : MonoBehaviour
    {
        
        [field: SerializeField]
        public float TargetY { get; private set; }
        void Awake()
        {
            TargetY = transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position.WithY(TargetY);
        }
    }
}