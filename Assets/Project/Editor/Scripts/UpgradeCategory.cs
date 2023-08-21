using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeCategory
{
    public string CategoryName;
    public int RequiredLevel;
    public List<UpgradeTemplate> Upgrades;
}
