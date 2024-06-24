using UnityEngine;
using CaptainCoder.UnityEngine;
namespace CaptainCoder.DarkFuel
{
    [RequireComponent(typeof(PlayerComponents))]
    public class PlayerJumpController : MonoBehaviour
    {
        private PlayerComponents _playerComponents;
        public Transform GroundCheck;
        public float CheckDistance = 1;
        public LayerMask GroundLayers;
        [field: SerializeField]
        public bool IsGrounded { get; private set; } = true;
        [field: SerializeField]
        public float JumpForce { get; set; } = 500;

        private bool _jumpStarted = false;

        // Start is called before the first frame update
        void Awake()
        {
            _playerComponents = GetComponent<PlayerComponents>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump") && IsGrounded)
            {
                _jumpStarted = true;
            }
        }

        void FixedUpdate()
        {
            CheckGround();
            if (_jumpStarted)
            {
                _jumpStarted = false;
                IsGrounded = false;
                _playerComponents.RigidBody.AddForce(Vector3.up * JumpForce);
            }
        }

        private void CheckGround()
        {
            Ray ray = new() { origin = GroundCheck.position, direction = Vector3.down };
            Vector3 extents = new (CheckDistance, CheckDistance, CheckDistance);
            bool isHit = Physics.BoxCast(GroundCheck.position, extents, Vector3.down, out RaycastHit hitInfo);
            if (isHit)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }   

        }

        void OnDrawGizmosSelected()
        {
            Vector3 extents = new (CheckDistance, CheckDistance, CheckDistance);
            Gizmos.DrawCube(GroundCheck.position, extents);
        }
    }
}