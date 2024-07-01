using System.Collections;
using System.Collections.Generic;
using CaptainCoder.DarkFuel;
using NaughtyAttributes;
using UnityEngine;

public class BackgroundOffsetController : MonoBehaviour
{
    public MeshRenderer Renderer;

    public Vector2 Offset;

    public void Update()
    {
        Renderer.material.SetTextureOffset("_MainTex", Offset);
    }

}