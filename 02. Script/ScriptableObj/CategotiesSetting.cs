using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CategotiesSetting : ScriptableObject
{
#if UNITY_EDITOR
    public List<string[]> catetories;

    public static CategotiesSetting CreateScriptableObject(string name)
    {
        string path = "Assets/Resources/" + name + ".asset";
        var exampleAsset = CreateInstance<CategotiesSetting>();

        //AssetDatabase.CreateAsset(exampleAsset, "Assets/Editor/DataFile1.asset");
        AssetDatabase.CreateAsset(exampleAsset, path);
        AssetDatabase.Refresh();

        return exampleAsset;
    }
#endif
}
