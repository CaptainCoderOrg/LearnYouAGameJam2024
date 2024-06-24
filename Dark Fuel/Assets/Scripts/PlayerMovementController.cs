using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody _rigidbody;    private Animator _animator;
    private float ForwardAxis => Input.GetAxis("Vertical");
    private float MaxSpeed = 10;
    public float Speed = 500;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward * Speed * ForwardAxis * Time.deltaTime);
        _rigidbody.velocity = _rigidbody.velocity.ClampXZMagnitude(MaxSpeed);
        _animator.SetFloat("Velocity", _rigidbody.velocity.XZ().normalized.magnitude);
    }
}

public static class Vector3Extensions
{
    public static Vector3 WithZ(this Vector3 vector, float z) => new(vector.x, vector.y, z);
    public static Vector3 WithXZ(this Vector3 vector, Vector3 other) => new(other.x, vector.y, other.z);
    public static Vector3 XZ(this Vector3 vector) => new(vector.x, 0, vector.z);
    public static Vector3 ClampXZMagnitude(this Vector3 vector, float max)
    {
        return vector.WithXZ(Vector3.ClampMagnitude(vector.XZ(), max));
    }
}
