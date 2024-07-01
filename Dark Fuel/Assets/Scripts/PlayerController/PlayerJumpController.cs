using UnityEngine;
using CaptainCoder.UnityEngine;
using System.Collections;
using System;
namespace CaptainCoder.DarkFuel
{
    [RequireComponent(typeof(PlayerComponents))]
    public class PlayerJumpController : MonoBehaviour
    {
        private PlayerComponents _playerComponents;
        private Rigidbody Rigidbody => _playerComponents.RigidBody;
        public Transform GroundCheck;
        public float CheckDistance = 1;
        public float MaxFallSpeed = 32;
        public Vector3 BoxCheckSize = new (.25f, .1f, .25f);
        public LayerMask GroundLayers;
        [field: SerializeField]
        public bool IsGrounded { get; private set; } = true;
        private bool _jumpStarted = false;
        public float JumpHeight => _playerComponents.Ability.JumpHeight;
        public float JumpDuration => _playerComponents.Ability.JumpDuration;
        public AnimationCurve JumpArch => _playerComponents.Ability.JumpArch;

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
                _playerComponents.ModelAnimator.SetBool("jumpStarted", true);
            }
        }

        void FixedUpdate()
        {
            CheckGround();
            if (_jumpStarted)
            {
                _jumpStarted = false;
                _playerComponents.ModelAnimator.SetBool("jumpStarted", false);
                IsGrounded = false;
                _playerComponents.ModelAnimator.SetBool("isGrounded", false);
                StartCoroutine(Jump());
            }
            float yVelocity = Math.Max(-MaxFallSpeed, Rigidbody.velocity.y);
            Rigidbody.velocity = Rigidbody.velocity.WithY(yVelocity);
        }
        public IEnumerator Jump()
        {
            WaitForFixedUpdate wait = new ();
            float duration = JumpDuration;
            float startY = transform.position.y;
            _playerComponents.Attach.Detach?.Invoke();
            while (duration > 0)
            {
                duration -= Time.fixedDeltaTime;
                float percent = 1 - (duration / JumpDuration);
                if (percent >= JumpArch[1].time && GroundRaycast())
                {
                    yield break;
                }
                float archPosition = JumpArch.Evaluate(percent) * JumpHeight;
                Rigidbody.MovePosition(transform.position.WithY(startY + archPosition));
                yield return wait;
            }
            Rigidbody.MovePosition(transform.position.WithY(startY));
        }

        private bool GroundRaycast(out RaycastHit hitInfo) => Physics.BoxCast(GroundCheck.position, BoxCheckSize, Vector3.down, out hitInfo, Quaternion.identity, CheckDistance, GroundLayers);
        private bool GroundRaycast() => Physics.BoxCast(GroundCheck.position, BoxCheckSize, Vector3.down, out RaycastHit hitInfo, Quaternion.identity, CheckDistance, GroundLayers);
        
        private void CheckGround()
        {
            if (GroundRaycast(out var hitInfo))
            {
                IsGrounded = true;
                _playerComponents.ModelAnimator.SetBool("isGrounded", true);
            }
            else
            {
                IsGrounded = false;
                _playerComponents.ModelAnimator.SetBool("isGrounded", false);
            }

        }
    }
}