using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoneyCounter))]
public class MoneyCounterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    
        MoneyCounter moneyCounter = (MoneyCounter) target;
    
        if (GUILayout.Button("Add Money"))
        {
            moneyCounter.AddMoney(1000);
        }
    }
}

