using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new Dropdown", menuName = "Create Dropdown Data")]
public class DropDownData : ScriptableObject
{
    public List<string> dropdownData;
}
