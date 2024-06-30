using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilityData", menuName = "Dark Fuel/Player Ability")]
public class PlayerAbilityData : ScriptableObject
{
    public AnimationCurve JumpArch;
    public float JumpHeight = 2;
    public float JumpDuration = 1;
    public Material LiquidMaterial;

}
