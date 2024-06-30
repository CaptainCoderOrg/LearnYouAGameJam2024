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
        public PlayerMovementController PlayerMovementController;
        public PlayerJumpController PlayerJumpController;
        public PlayerAbilityData Ability;
        public Transform LiquidTransform;
        public MeshRenderer LiquidRenderer;

        void Awake()
        {
            RigidBody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
            Attach = GetComponent<AttachToTileController>();
            CameraFollower = FindFirstObjectByType<CameraFollower>();
            PlayerMovementController = GetComponent<PlayerMovementController>();
            PlayerJumpController = GetComponent<PlayerJumpController>();
        }

        public void Lock()
        {
            RigidBody.velocity = Vector3.zero;
            Animator.SetFloat("Velocity", 0);
            Animator.SetBool("isGrounded", true);
            PlayerMovementController.enabled = false;
            PlayerJumpController.enabled = false;
        }

        public void Unlock()
        {
            PlayerMovementController.enabled = true;
            PlayerJumpController.enabled = true;
        }

    }
}