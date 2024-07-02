using System.Collections;
using CaptainCoder.DarkFuel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeansRemainingController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _label;
    public string Text
    {
        get => _label.text;
        set => _label.text = value;
    }

    private void Awake()
    {
        _label = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void RegisterLevelController(LevelController controller)
    {
        UpdateText(controller.BeansCollected, controller.TotalBeans);
        controller.OnBeansUpdated += UpdateText;
    }
    private void UpdateText(int collected, int total)
    {
        _label.text = $"{collected} / {total}";
    }
}