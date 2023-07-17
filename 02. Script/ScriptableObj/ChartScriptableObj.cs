using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ChartData", menuName = "ChartData")]
public class ChartScriptableObj : ScriptableObject
{
    public int chartCount;
    public string symbolName;
    public string description;
    public int chartNum;
}
