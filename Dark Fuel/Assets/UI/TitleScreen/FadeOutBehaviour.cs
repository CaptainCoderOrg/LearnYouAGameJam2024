using UnityEngine;
using UnityEngine.Events;

public class FadeOutBehaviour : StateMachineBehaviour
{
    public UnityEvent OnFadeFinished;
    public UnityEvent OnFadeStarted;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnFadeStarted.Invoke();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnFadeFinished.Invoke();
    }

}