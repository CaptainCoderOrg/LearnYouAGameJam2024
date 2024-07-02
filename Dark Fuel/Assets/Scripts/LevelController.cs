using System.Collections;
using CaptainCoder.DarkFuel;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string NextScene;
    [SerializeField]
    private int _beansCollected = 0;
    public event System.Action<int, int> OnBeansUpdated;
    public int BeansCollected
    {
        get => _beansCollected;
        set
        {
            _beansCollected = value;
            OnBeansUpdated?.Invoke(_beansCollected, TotalBeans);
            if (BeansRemaining == 0)
            {
                Win();
            }
        }
    }
    public int TotalBeans;
    public int BeansRemaining => TotalBeans - BeansCollected;
    public PlayerComponents Player;
    public HUDController HUD;

    public void Awake()
    {
        Player = FindFirstObjectByType<PlayerComponents>();
        FindHUD();
    }

    [Button("Spawn")]
    public void Spawn()
    {
        Player.PlayerAnimator.SetTrigger("Spawn");
    }

    [Button("CompleteLevel")]
    public void Win()
    {
        HUD.Win(Player, NextScene);
    }

    public void FindHUD()
    {
        if (FindFirstObjectByType<HUDController>() is HUDController hud)
        {
            HUD = hud;
        }
        else
        {
            StartCoroutine(LoadHUD());
        }
    }

    private IEnumerator LoadHUD()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            yield return null;
        }
        TitleScreenController titleScreen = FindFirstObjectByType<TitleScreenController>();
        titleScreen.Hide();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        Player.PlayerAnimator.SetTrigger("Spawn");
        HUD = FindFirstObjectByType<HUDController>();
        Debug.Assert(HUD != null);
        HUD.ShowReady();
    }
}
