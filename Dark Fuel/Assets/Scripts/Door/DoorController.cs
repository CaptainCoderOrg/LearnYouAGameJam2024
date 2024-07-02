using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public DoorController Other;
    public Animator Animator;
    public bool StartOpen = false;
    public AudioSource SFX;
    public bool IsOpen
    {
        get => Animator.GetBool("isOpen");
        set
        {
            Animator.SetBool("isOpen", value);
        }
    }
    void Awake()
    {
        SFX = GetComponentInChildren<AudioSource>();
        Animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        if (StartOpen)
        {
            Open();
        }
    }

    [Button("Open")]
    public void Open()
    {
        if (!IsOpen)
        {
            SFX?.Play();
        }
        IsOpen = true;
        Other.IsOpen = true;
    }
    [Button("Close")]
    public void Close()
    {
        IsOpen = false;
        Other.IsOpen = false;
    }
}
