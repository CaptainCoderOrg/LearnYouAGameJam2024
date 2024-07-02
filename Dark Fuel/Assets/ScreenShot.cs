#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
             ScreenCapture.CaptureScreenshot("Screenshot.png");
        }
    }
}
#endif
