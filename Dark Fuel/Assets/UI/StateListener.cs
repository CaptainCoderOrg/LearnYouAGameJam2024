using UnityEngine;
using UnityEngine.Events;

public class StateListener : StateMachineBehaviour
{
    public UnityEvent OnStateFinished;
    public UnityEvent OnStateStarted;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnStateStarted.Invoke();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnStateFinished.Invoke();
    }

}