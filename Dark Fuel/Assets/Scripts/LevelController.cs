using System.Collections;
using System.Collections.Generic;
using CaptainCoder.DarkFuel;
using UnityEngine;

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
    }
}
