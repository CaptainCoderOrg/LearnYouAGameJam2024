using UnityEngine;
using CaptainCoder.UnityEngine;
namespace CaptainCoder.DarkFuel
{

    [RequireComponent(typeof(PlayerComponents))]
    public class PlayerMovementController : MonoBehaviour
    {
        private PlayerComponents _playerComponents;
        private float ForwardAxis => Input.GetAxis("Vertical");
        private float RightAxis => Input.GetAxis("Horizontal");
        private float MaxSpeed = 10;
        public float Speed = 500;
        // Start is called before the first frame update
        void Awake()
        {
            _playerComponents = GetComponent<PlayerComponents>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _playerComponents.RigidBody.AddForce(transform.forward * Speed * ForwardAxis * Time.deltaTime);
            _playerComponents.RigidBody.AddForce(transform.right * Speed * RightAxis * Time.deltaTime);
            _playerComponents.RigidBody.velocity = _playerComponents.RigidBody.velocity.ClampXZMagnitude(MaxSpeed);
            _playerComponents.Animator.SetFloat("Velocity", _playerComponents.RigidBody.velocity.XZ().normalized.magnitude);
        }
    }
}