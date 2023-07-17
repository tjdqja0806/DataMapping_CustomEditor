using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoadAssetBundles : MonoBehaviour
{
    public AllDataScriptableObj loadedAsset;

    public ObjDataScritableObj objAsset;

    void Awake()
    {
        //Debug.Log(Application.persistentDataPath);
        string path = Path.Combine(Application.persistentDataPath + "/AssetBundles/", "dataassetbundles");

        var assetBundle = AssetBundle.LoadFromFile(path);
        GameObject allDataManager = assetBundle.LoadAsset<GameObject>("AllDataManager");
        Instantiate(allDataManager);

        objAsset = assetBundle.LoadAsset<ScriptableObject>("TestObj") as ObjDataScritableObj;
        ModelLoad();
    }

    void ModelLoad()
    {
        string path = Path.Combine(Application.persistentDataPath + "/AssetBundles/", "eqipment");

        var modelAsset = AssetBundle.LoadFromFile(path);
        GameObject model = modelAsset.LoadAsset<GameObject>("Model");
        model.transform.localScale = new Vector3(15, 15, 15);
        Instantiate(model);


    }
}
