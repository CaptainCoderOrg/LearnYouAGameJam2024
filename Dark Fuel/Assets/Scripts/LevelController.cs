using System.Collections;
using System.Collections.Generic;
using CaptainCoder.DarkFuel;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private int _beansCollected = 0;
    public int BeansCollected
    {
        get => _beansCollected;
        set
        {
            _beansCollected = value;
            if (BeansRemaining == 0)
            {
                Player.Win();
            }
        }
    }
    public int TotalBeans;
    public int BeansRemaining => TotalBeans - BeansCollected;
    public PlayerComponents Player;

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

    public void FindHUD()
    {
        if (FindFirstObjectByType<HUDController>() is HUDController hud)
        {

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
        Player.PlayerAnimator.SetTrigger("Spawn");
    }
}
