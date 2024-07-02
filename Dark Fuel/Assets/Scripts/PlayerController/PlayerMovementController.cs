using UnityEngine;
using CaptainCoder.UnityEngine;
using System.Collections;
namespace CaptainCoder.DarkFuel
{

    [RequireComponent(typeof(PlayerComponents))]
    public class PlayerMovementController : MonoBehaviour
    {
        public ControlSettingsData Controls;
        public Vector3 InputDirection { get; private set; }
        private PlayerComponents _playerComponents;
        private float ForwardAxis => Input.GetAxis("Vertical");
        private float RightAxis => -Input.GetAxis("Horizontal");
        public float Speed = 20;
        // Start is called before the first frame update
        void Awake()
        {
            _playerComponents = GetComponent<PlayerComponents>();
        }

        void Update()
        {
            InputDirection = new (ForwardAxis, 0, RightAxis);
            if (!Controls.UseIsoMovement())
            {
                InputDirection = Quaternion.AngleAxis(-45, Vector3.up) * InputDirection;
            }
            if (InputDirection.magnitude > 0.1)
            {
                StopAllCoroutines();
                StartCoroutine(RotateCup(InputDirection));
            }            
        }

        public float DirectionChangeSpeed = 0.1f;
        private IEnumerator RotateCup(Vector3 inputDirection)
        {
            float timeRemaining = DirectionChangeSpeed;
            Quaternion startRotation = _playerComponents.Model.transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            while (timeRemaining > 0)
            {
                float percent = 1 - (timeRemaining / DirectionChangeSpeed);
                _playerComponents.Model.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percent);
                yield return new WaitForEndOfFrame();
                timeRemaining -= Time.deltaTime;
            }
            _playerComponents.Model.transform.rotation = targetRotation;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _playerComponents.RigidBody.velocity = _playerComponents.RigidBody.velocity.WithXZ(InputDirection * Speed);
            _playerComponents.RigidBody.velocity = _playerComponents.RigidBody.velocity.ClampXZMagnitude(Speed);
            _playerComponents.ModelAnimator.SetFloat("Velocity", _playerComponents.RigidBody.velocity.XZ().normalized.magnitude);
        }
    }
}