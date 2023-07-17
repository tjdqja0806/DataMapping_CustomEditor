using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AllDataPrefab : MonoBehaviour
{
    public string tagName;
    public string description;
    public TextMeshProUGUI tagNameText;
    public TextMeshProUGUI descriptionText;
    public string uiType;

    private ObjData script;

    public void Click()
    {
        Debug.Log(uiType);
        script = GameObject.Find(uiType).GetComponent<ObjData>();
        /*script.tagNameText.text = tagName.text;
        script.descriptionText.text = description.text;*/
        script.objData.tagName = tagName;
        script.objData.description = description;
    }
}
