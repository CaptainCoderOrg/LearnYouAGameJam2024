using UnityEngine;
using CaptainCoder.UnityEngine;
namespace CaptainCoder.DarkFuel
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerComponents : MonoBehaviour
    {
        public CameraFollower CameraFollower;
        public GameObject LastSolidGround;
        public Rigidbody RigidBody; 
        public Animator Animator;
        public AttachToTileController Attach;
        public GameObject Model;

        void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
            Attach = GetComponent<AttachToTileController>();
            CameraFollower = FindFirstObjectByType<CameraFollower>();
        }

    }
}