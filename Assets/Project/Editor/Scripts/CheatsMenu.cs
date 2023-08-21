using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheatsMenu : MonoBehaviour
{
    [MenuItem("Cheats/Add Money")]
    private static void AddMoney()
    {
        MoneyCounter moneyCounter = FindObjectOfType<MoneyCounter>();
        if (moneyCounter != null)
        {
            moneyCounter.AddMoney(1000);
        }
    }

    [MenuItem("Cheats/Next level")]
    private static void NextLevel()
    {
        ChangeLevel(1);
    }
    
    [MenuItem("Cheats/Previous level")]
    private static void PreviousLevel()
    {
        ChangeLevel(-1);
    }
    
    private static void ChangeLevel(int delta)
    {
        PlayerData playerData = Resources.Load<PlayerData>("PlayerData");
        if (playerData != null)
        {
            int newLevel = Mathf.Clamp(playerData.Level + delta, 0, int.MaxValue);
            playerData.SetLevel(newLevel);

            if (delta > 0)
                Debug.Log("Level Increased: " + playerData.Level);
            else if (delta < 0)
                Debug.Log("Level Decreased: " + playerData.Level);
        }
    }
}
