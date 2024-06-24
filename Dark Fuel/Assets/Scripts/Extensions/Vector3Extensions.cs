using UnityEngine;
namespace CaptainCoder.UnityEngine
{
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
}