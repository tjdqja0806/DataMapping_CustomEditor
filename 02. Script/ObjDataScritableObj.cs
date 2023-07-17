using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class ObjDataScritableObj : ScriptableObject
{
    public string tagName;
    public string description;

#if UNITY_EDITOR
    public static void CreateObjectDataFile(string name)
    {
        string path = "Assets/Resources/" + name + ".asset";
        var exampleAsset = CreateInstance<ObjDataScritableObj>();
        AssetDatabase.CreateAsset(exampleAsset, path);
        AssetDatabase.Refresh();
    }
#endif
}
