using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new DataInfo", menuName = "DataInfo")]
public class DataInfo : ScriptableObject
{
    public string[] symbolName;
    public string[] equipmentName;
    public string[] description;

    public List <string[]> dataForms;
    public List<string[]> equipmentForms;
}
 