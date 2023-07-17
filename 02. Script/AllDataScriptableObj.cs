using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class AllDataScriptableObj : ScriptableObject
{
    public string[] tagName;
    public string[] description;

    public List<Form> forms;

    public struct Form
    {
        public string plant;
        public string system;
        public string component;
        public string tag;
        public string description;
        public string unit;
        public string limitLo;
        public string limitHi;
    }

    public List<Form> realData => forms;
    public string[] tagNameRead => tagName;
    public string[] descriptionRead => description;
#if UNITY_EDITOR
    public static void CreateAllDataFile(string name)
    {

        string path = "Assets/Resources/" + name + ".asset";
        var exampleAsset = CreateInstance<AllDataScriptableObj>();
        AssetDatabase.CreateAsset(exampleAsset, path);
        AssetDatabase.Refresh();
    }
#endif

#if UNITY_EDITOR
    public static void SaveFile()
    {
        AssetDatabase.SaveAssets();
    }
#endif
}
