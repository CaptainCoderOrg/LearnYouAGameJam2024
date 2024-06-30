using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KettlePourStateMachineBehaviour : StateMachineBehaviour
{
    public UnityEvent OnFinished;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool("isPouring", false);
        OnFinished?.Invoke();
    }
}
