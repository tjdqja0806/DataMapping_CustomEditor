using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class ObjData : MonoBehaviour
{
    //ScriptableObj 
    public ObjDataScritableObj objData;
    string objName;

    //Data
    public TextMeshProUGUI tagNameText;
    public TextMeshProUGUI descriptionText;

    private string tagName;
    private string description;

    //public AllDataUIScript dataSeleteUI;
    public AllDataModule allDataModule;
    void Start()
    {
        objName = gameObject.name;

        objData = Resources.Load(objName, typeof(ScriptableObject)) as ObjDataScritableObj;

        //objData = GameObject.Find("AssetBundleManager").GetComponent<LoadAssetBundles>().objAsset;

        tagName = objData.tagName;
        description = objData.description;
        tagNameText.text = tagName;
        descriptionText.text = description;
    }

    void Update()
    {
        tagNameText.text = objData.tagName;
        descriptionText.text = objData.description;
    }

    public void Setting()
    {
        allDataModule.gameObject.SetActive(true);
        allDataModule.UISettingClick(gameObject.name);
    }
}
