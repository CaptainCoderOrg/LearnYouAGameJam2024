using System.Collections;
using System.Collections.Generic;
using CaptainCoder.DarkFuel;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{
    public HUDController HUDController;   
    public Camera TitleSceneCamera;
    public GameObject[] TitleScreenObjects;
    
    public void Play(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    private IEnumerator LoadLevel(string scene)
    {   
        HUDController.FadeOut();
        while (!HUDController.isFadedOut) { yield return null; }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone) { yield return null; }
        LevelController levelController = FindFirstObjectByType<LevelController>();
        Hide();
        HUDController.BeansRemaining.gameObject.SetActive(true);
        HUDController.FadeIn();
        HUDController.ShowReady();
        
    }

    public void Hide()
    {
        foreach (GameObject obj in TitleScreenObjects)
        {
            obj.SetActive(false);
        }
        TitleSceneCamera.gameObject.SetActive(false);
    }

    public void Show()
    {
        foreach (GameObject obj in TitleScreenObjects)
        {
            obj.SetActive(true);
        }
        TitleSceneCamera.gameObject.SetActive(true);
    }

}