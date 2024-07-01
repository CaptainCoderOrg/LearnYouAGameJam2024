using UnityEngine;

public class HUDController : MonoBehaviour
{
       
    public Animator CanvasAnimator;
    public Animator ReadyAnimator;
    public bool isFadedOut = false;
    public GameObject Fade;
    public LevelController LevelController;
    public bool isReady = false;

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
        LevelController = null;
        LevelController = FindFirstObjectByType<LevelController>();
        Debug.Assert(LevelController != null);
        ReadyAnimator.SetTrigger("Show");
        isReady = true;
    }

    public void DisableFade() => Fade.SetActive(false);

    public void EnableFade() => Fade.SetActive(true);

    public void Update()
    {
        if (!isReady) { return; }
        if(Input.anyKeyDown)
        {
            ReadyAnimator.SetTrigger("Spawn");
            isReady = false;
            LevelController.Spawn();
        }
    }

}