using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchBoxScript : MonoBehaviour
{
    public TMP_Dropdown searchBoxTypeDropdown;
    public TMP_InputField searchBox;
    [HideInInspector] public string searchBoxType;

    void OnEnable()
    {
        SearchBoxReset();
    }

    public void SearchBoxReset()
    {
        DataSettingAlgorism.DropdownValueReset(searchBoxTypeDropdown);
        searchBox.text = null;
        searchBoxType = searchBoxTypeDropdown.options[searchBoxTypeDropdown.value].text;
    }
}
