using System.Collections;
using System.Collections.Generic;
using CaptainCoder.DarkFuel;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public CameraFollower CameraFollower;
    // Start is called before the first frame update
    void Awake()
    {
        CameraFollower = FindFirstObjectByType<CameraFollower>();
    }
}
