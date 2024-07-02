using UnityEngine;
public class ControlButtonColor : MonoBehaviour
{
    public ControlSettingsData Controls;
    public Color IsoColor;
    public Color NonIsoColor;
    public UnityEngine.UI.Image Image;

    public void Update()
    {
        Image.color = Controls.useIsoMovement ? IsoColor : NonIsoColor;
    }

    public void TurnOnIso() => Controls.useIsoMovement = true;
    public void TurnOffIso() => Controls.useIsoMovement = false;
}