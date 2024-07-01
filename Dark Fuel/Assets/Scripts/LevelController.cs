using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int BeansCollected;
    public int TotalBeans;
    public int BeansRemaining => TotalBeans - BeansCollected;
    
}
