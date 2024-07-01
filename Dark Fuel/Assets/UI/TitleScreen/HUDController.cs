using UnityEngine;

public class HUDController : MonoBehaviour
{
       
    public Animator CanvasAnimator;
    public Animator ReadyAnimator;
    public bool isFadedOut = false;
    public GameObject Fade;

    void Awake()
    {
        FadeOutBehaviour fadeOutBehaviour = CanvasAnimator.GetBehaviour<FadeOutBehaviour>();
        fadeOutBehaviour.OnFadeStarted.AddListener(() => isFadedOut = false);
        fadeOutBehaviour.OnFadeFinished.AddListener(() => isFadedOut = true);
    }

    public void FadeIn()
    {
        CanvasAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        isFadedOut = false;
        CanvasAnimator.SetTrigger("FadeOut");
    }

    public void ShowReady()
    {
        ReadyAnimator.SetTrigger("Show");
    }

    public void DisableFade() => Fade.SetActive(false);

    public void EnableFade() => Fade.SetActive(true);



}