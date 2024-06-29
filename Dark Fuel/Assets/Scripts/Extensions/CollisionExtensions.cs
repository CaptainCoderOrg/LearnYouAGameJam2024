using System.Collections.ObjectModel;
using UnityEngine;
public static class CollisionExtensions
{

    public static bool IsAbove(this Collision collision, float threshold = 0.1f)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y <= (-1.0 + threshold))
            {
                return true;
            }
        }
        return false;
    }
}