using UnityEngine;
using CaptainCoder.UnityEngine;
namespace CaptainCoder.DarkFuel
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerComponents : MonoBehaviour
    {
        public Rigidbody RigidBody; 
        public Animator Animator;
        public GameObject Model;

        public static bool ChakrasAligned { get; internal set; } = false;

        void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
        }

    }
}