using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Animator Animator;
    public TextMeshProUGUI DialogueText;
    void Awake()
    {
        Animator = GetComponent<Animator>();        
    }

    [Button("Show")]
    public void Show()
    {
        Animator.SetTrigger("Show");
    }

    public void Show(string message)
    {
        DialogueText.text = message;
        Animator.SetTrigger("Show");
    }
    
    [Button("Hide")]
    public void Hide()
    {
        Animator.SetTrigger("Hide");
    }
}
