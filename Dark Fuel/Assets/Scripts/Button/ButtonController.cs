using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public UnityEvent<bool> OnChange;
    private ButtonCollisionController _collision;
    public Animator Animator;
    public bool IsPressed 
    { 
        get => Animator.GetBool("isPressed");
        set 
        {
            Animator.SetBool("isPressed", value);
            OnChange.Invoke(value);
        }
    }
    void Awake()
    {
        _collision = GetComponentInChildren<ButtonCollisionController>();
        _collision.OnPressed.AddListener(Press);

        Animator = GetComponentInChildren<Animator>();
    }

    [Button("Press")]
    void Press()
    {
        IsPressed = true;
    }

    [Button("Reset")]
    void Reset()
    {
        IsPressed = false;
    }
}
