using System.Collections;
using CaptainCoder.DarkFuel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
       
    public Animator CanvasAnimator;
    public Animator ReadyAnimator;
    public Animator CompleteAnimator;
    public bool isFadedOut = false;
    public GameObject Fade;
    public LevelController LevelController;
    public bool isReady = false;
    public TitleScreenController TitleScreen;
    public DialogueController Dialogue;
    public BeansRemainingController BeansRemaining;
    

    void Awake()
    {
        Dialogue = FindFirstObjectByType<DialogueController>();
        Debug.Assert(Dialogue != null);
        TitleScreen = FindFirstObjectByType<TitleScreenController>();
        StateListener fadeOutBehaviour = CanvasAnimator.GetBehaviour<StateListener>();
        fadeOutBehaviour.OnStateStarted.AddListener(() => isFadedOut = false);
        fadeOutBehaviour.OnStateFinished.AddListener(() => isFadedOut = true);
        BeansRemaining = GetComponentInChildren<BeansRemainingController>();
        Debug.Assert(BeansRemaining != null);
        BeansRemaining.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        CanvasAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        isFadedOut = false;
        CanvasAnimator.SetTrigger("FadeOut");
        BeansRemaining.gameObject.SetActive(false);
    }

    public void ShowReady()
    {
        LevelController = null;
        LevelController = FindFirstObjectByType<LevelController>();
        Debug.Assert(LevelController != null);
        ReadyAnimator.SetTrigger("Show");
        BeansRemaining.gameObject.SetActive(true);
        BeansRemaining.RegisterLevelController(LevelController);
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

    public void Win(PlayerComponents player, string nextScene) => StartCoroutine(WinAnimation(player, nextScene));

    private IEnumerator WinAnimation(PlayerComponents player, string nextScene)
    {
        player.PlayerAnimator.SetTrigger("Won");
        CompleteAnimator.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        FadeOut();
        while (!isFadedOut)
        {
            yield return null;
        }

        CompleteAnimator.gameObject.SetActive(false);
        Scene toUnload = player.gameObject.scene;
        AsyncOperation unloading = SceneManager.UnloadSceneAsync(toUnload);
        while (!unloading.isDone) { yield return null; }
        if (nextScene == "Main")
        {
            TitleScreen.Show();
            FadeIn();
        }
        else
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            while (!loading.isDone) { yield return null; }
            BeansRemaining.gameObject.SetActive(true);
            FadeIn();
            ShowReady();
        }
        
    }
}