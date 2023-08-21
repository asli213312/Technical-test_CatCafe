using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Create PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int level;
    public event Action<int> OnLevelChanged;

    public int Level
    {
        get => level;
        private set => level = value;
    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        OnLevelChanged?.Invoke(level);
    }
}
