using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControlSettings", menuName = "Dark Fuel/Control Settings")]
public class ControlSettingsData : ScriptableObject
{
    public bool useIsoMovement = false;

    public bool UseIsoMovement() => useIsoMovement;

}
