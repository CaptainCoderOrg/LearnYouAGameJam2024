using System.Collections;
using System.Collections.Generic;
using CaptainCoder.DarkFuel;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuController : MonoBehaviour
{
    private bool _isVisible = false;

    public Animator Animator;
    void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void Toggle()
    {
        if (_isVisible)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    [Button("Show")]
    public void Show()
    {
        Animator.SetTrigger("Show");
        _isVisible = true;
    }

    [Button("Hide")]
    public void Hide()
    {
        Animator.SetTrigger("Hide");
        _isVisible = false;
    }
}