using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KettlePourStateMachineBehaviour : StateMachineBehaviour
{
    public UnityEvent OnFinished;
    public UnityEvent<float> OnFrameUpdate;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool("isPouring", false);
        OnFinished?.Invoke();
        OnFrameUpdate.Invoke(1);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        OnFrameUpdate?.Invoke(stateInfo.normalizedTime);
    }
}
