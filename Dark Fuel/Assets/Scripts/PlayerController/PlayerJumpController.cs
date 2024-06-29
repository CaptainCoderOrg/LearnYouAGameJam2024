using UnityEngine;
using CaptainCoder.UnityEngine;
using System.Collections;
namespace CaptainCoder.DarkFuel
{
    [RequireComponent(typeof(PlayerComponents))]
    public class PlayerJumpController : MonoBehaviour
    {
        private PlayerComponents _playerComponents;
        private Rigidbody Rigidbody => _playerComponents.RigidBody;
        public Transform GroundCheck;
        public float CheckDistance = 1;
        public Vector3 BoxCheckSize = new (.25f, .1f, .25f);
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
                _playerComponents.Animator.SetBool("jumpStarted", true);
            }
        }

        void FixedUpdate()
        {
            CheckGround();
            if (_jumpStarted)
            {
                _jumpStarted = false;
                _playerComponents.Animator.SetBool("jumpStarted", false);
                IsGrounded = false;
                _playerComponents.Animator.SetBool("isGrounded", false);
                StartCoroutine(Jump());
            }
        }
        
        public float JumpDuration = 0.75f;
        public AnimationCurve JumpArch;
        public IEnumerator Jump()
        {
            WaitForFixedUpdate wait = new ();
            float duration = JumpDuration;
            float startY = transform.position.y;
            while (duration > 0)
            {
                duration -= Time.fixedDeltaTime;
                float percent = 1 - (duration / JumpDuration);
                if (percent >= JumpArch[1].time && GroundRaycast())
                {
                    yield break;
                }
                float archPosition = JumpArch.Evaluate(percent) * JumpForce;
                Rigidbody.MovePosition(transform.position.WithY(startY + archPosition));
                Debug.Log(archPosition);
                yield return wait;
            }
            Rigidbody.MovePosition(transform.position.WithY(startY));
        }

        private bool GroundRaycast(out RaycastHit hitInfo) => Physics.BoxCast(GroundCheck.position, BoxCheckSize, Vector3.down, out hitInfo, Quaternion.identity, CheckDistance, GroundLayers);
        private bool GroundRaycast() => Physics.BoxCast(GroundCheck.position, BoxCheckSize, Vector3.down, out RaycastHit hitInfo, Quaternion.identity, CheckDistance, GroundLayers);
        
        private void CheckGround()
        {
            if (GroundRaycast())
            {
                IsGrounded = true;
                _playerComponents.Animator.SetBool("isGrounded", true);
            }
            else
            {
                IsGrounded = false;
                _playerComponents.Animator.SetBool("isGrounded", false);
            }

        }
    }
}