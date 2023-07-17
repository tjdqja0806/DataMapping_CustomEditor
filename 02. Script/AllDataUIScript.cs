using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
#endif
using UnityEngine;

public class AllDataUIScript : MonoBehaviour
{
    AllDataObject allData;

    public AllDataPrefab dataPrefab;
    public RectTransform parents;

    [Header("SearchType")]
    public TMP_Dropdown searchTypeDropdown;
    public GameObject[] searchType_Grp;

    [Header("Dropdown")]
    public List<string> dropdownString = new List<string>();
    public TMP_Dropdown[] dropdown;
    public TextMeshProUGUI totalDropdownCountText;

    [Header("SearchBox")]
    public TMP_InputField searchBox;
    public TMP_Dropdown searchBoxTypeDropdown;

    //Dropdown
    private int curDropdown = 0;
    private string seletedDropdown = "None";
    private int totalDropdownCount = 0;
    private int maxDropdownCount = 5;
    private List<string> searchedString = new List<string>();
    private List<string[]> devidedStringList = new List<string[]>();
    private List<int>[] curList;

    private string settingUIName;

    void Start()
    {
        allData = GameObject.Find("AllDataManager(Clone)").GetComponent<AllDataObject>();
        DevideString();
        for (int i = 0; i < maxDropdownCount; i++)
        {
            searchedString.Add("None");
        }
        curList = new List<int>[maxDropdownCount];

        gameObject.SetActive(false);
    }
    void Update()
    {
        totalDropdownCountText.text = (totalDropdownCount + 1).ToString();
    }
    //UI Setting Click Event
    public void SettingClick(string uiName)
    {
        gameObject.SetActive(true);
        settingUIName = uiName;
        CategoryOn();
    }

    #region Category/Dropdown
    //Add Category Event
    public void CategoryCountUp()
    {
        if (totalDropdownCount == maxDropdownCount - 1)
        {
            totalDropdownCount = maxDropdownCount;
        }
        else
        {
            totalDropdownCount += 1;
        }
        if (totalDropdownCount < maxDropdownCount)
            curList[totalDropdownCount] = new List<int>();

        CategoryOn();
    }

    //Category Dropdown SetActive(true) Event
    private void CategoryOn()
    {
        dropdown[totalDropdownCount].gameObject.SetActive(true);
        dropdownString.Clear();
        if (totalDropdownCount == 0 && curDropdown == 0)
        {
            curList[totalDropdownCount] = new List<int>();
            for (int i = 0; i < devidedStringList.Count; i++)
            {
                dropdownString.Add(devidedStringList[i][totalDropdownCount]);
                curList[totalDropdownCount].Add(i);
            }
            DataSettingAlgorism.DropDownOptionInsert(dropdown[totalDropdownCount], dropdownString);
        }
        else
        {
            if (searchedString[totalDropdownCount - 1] == "None")
            {
                DataSettingAlgorism.DropDownOptionNone(dropdown[totalDropdownCount]);
            }
            else
            {
                for (int i = 0; i < curList[totalDropdownCount - 1].Count; i++)
                {
                    if (devidedStringList[curList[totalDropdownCount - 1][i]][totalDropdownCount - 1].Contains(searchedString[totalDropdownCount - 1]))
                    {
                        dropdownString.Add(devidedStringList[i][totalDropdownCount]);
                        curList[totalDropdownCount].Add(i);
                    }
                }
                DataSettingAlgorism.DropDownOptionInsert(dropdown[totalDropdownCount], dropdownString);
            }
        }
    }

    //DropDown Option Click Event
    public void DropdownSelete(TMP_Dropdown temp)
    {
        for (int i = 0; i < dropdown.Length; i++)
        {
            if (temp.name == dropdown[i].name)
            {
                curDropdown = i;
            }
        }

        seletedDropdown = temp.options[temp.value].text;

        if(searchedString[curDropdown] == "None")
        {
            searchedString[curDropdown] = seletedDropdown;
            CategoryOption();
        }
        else
        {
            searchedString[curDropdown] = seletedDropdown;
            DropdownSeleteAgain();
        }
    }

    //Add DropDown Options
    private void CategoryOption()
    {
        dropdownString.Clear();

        if (curDropdown < totalDropdownCount)
        {
            for (int i = 0; i < curList[curDropdown].Count; i++)
            {
                if (devidedStringList[curList[curDropdown][i]][curDropdown].Contains(searchedString[curDropdown]))
                {
                    dropdownString.Add(devidedStringList[curList[curDropdown][i]][curDropdown + 1]);
                    curList[curDropdown + 1].Add(curList[curDropdown][i]);
                }
            }
            if (dropdown[curDropdown + 1] != null)
                DataSettingAlgorism.DropDownOptionInsert(dropdown[curDropdown + 1], dropdownString);
        }
    }
    //DropDown Option Click Again
    private void DropdownSeleteAgain()
    {
        dropdownString.Clear();

        for(int i = curDropdown + 1; i <= totalDropdownCount; i++)
        {
            if (i == curDropdown + 1)
            {
                dropdown[i].captionText.text = "None";
                dropdown[i].value = 0;
                curList[i].Clear();

                for (int j = 0; j < curList[curDropdown].Count; j++)
                {
                    if (devidedStringList[curList[curDropdown][j]][curDropdown].Contains(searchedString[curDropdown]))
                    {
                        dropdownString.Add(devidedStringList[curList[curDropdown][j]][i]);
                        curList[i].Add(curList[curDropdown][j]);
                    }
                }
                if (dropdown[i] != null)
                {
                    Debug.Log("ReSetting");
                    DataSettingAlgorism.DropDownOptionInsert(dropdown[i], dropdownString);
                }
            }
            else
            {
                dropdown[i].captionText.text = "None";
                dropdown[i].value = 0;
                searchedString[i] = "None";
                curList[i].Clear();
                DataSettingAlgorism.DropDownOptionNone(dropdown[i]);
            }       
        }

        /* if (curDropdown < totalDropdownCount)
         {
             for (int i = 0; i < curList[curDropdown].Count; i++)
             {
                 if (devidedStringList[curList[curDropdown][i]][curDropdown].Contains(searchedString[curDropdown]))
                 {
                     dropdownString.Add(devidedStringList[curList[curDropdown][i]][curDropdown + 1]);
                     curList[curDropdown + 1].Add(curList[curDropdown][i]);
                 }
             }
             if (dropdown[curDropdown + 1] != null)
             {
                 Debug.Log("ReSetting");
                 DataSettingAlgorism.DropDownOptionInsert(dropdown[curDropdown + 1], dropdownString);
             }
         }*/
    }
    #endregion


    #region DataInstantiate
    //Instat Data Event
    public void InstantiateData()
    {
        //ScrollView 초기화 후 인스턴스 생성
        ClearParent();
        if(searchTypeDropdown.value == 0 && searchedString[0] != "None")  
        {
            CategoryDataInstantiate();
        }
        else if(searchTypeDropdown.value == 1 && searchBox.text != null)
        {
            SearchBoxInstantiate();
        }
        else
        {
            AllDataInstantiate();
        }
    }

    private void CategoryDataInstantiate()
    {
        int tempCount = searchedString.IndexOf("None") - 1;

        Debug.Log(curList[tempCount].Count);
        for (int i = 0; i < curList[tempCount].Count; i++)
        {
            if (devidedStringList[curList[tempCount][i]][tempCount].Contains(searchedString[tempCount]))
            {
                Debug.Log(curList[tempCount][i]);
                AllDataPrefab instant = Instantiate(dataPrefab);
                instant.transform.SetParent(parents, false);
                instant.tagName = allData.tagName[curList[tempCount][i]];
                instant.description = allData.description[curList[tempCount][i]];
                instant.tagNameText.text = allData.tagName[curList[tempCount][i]]; ;
                instant.descriptionText.text = allData.description[curList[tempCount][i]];
                instant.uiType = settingUIName;
            }
        }
    }

    private void AllDataInstantiate()
    {
        for(int i = 0; i < allData.tagName.Length; i++)
        {
            AllDataPrefab instant = Instantiate(dataPrefab);
            instant.transform.SetParent(parents, false);
            instant.tagName = allData.tagName[i];
            instant.description = allData.description[i];
            instant.tagNameText.text = allData.tagName[i]; ;
            instant.descriptionText.text = allData.description[i];
            instant.uiType = settingUIName;
        }
    }
    
    private void SearchBoxInstantiate()
    {
        List<int> tempNum = new List<int>();

        switch (searchBoxTypeDropdown.value)
        {
            case 0://TagName
                tempNum = DataSettingAlgorism.SearchBoxSearch(searchBox.text, allData.tagName);
                break;
            case 1://Description
                tempNum = DataSettingAlgorism.SearchBoxSearch(searchBox.text, allData.description);
                break;
        }

        for (int i = 0; i < tempNum.Count; i++)
        {
            AllDataPrefab instant = Instantiate(dataPrefab);
            instant.transform.SetParent(parents, false);
            instant.tagName = allData.tagName[tempNum[i]];
            instant.description = allData.description[tempNum[i]];
            instant.tagNameText.text = allData.tagName[tempNum[i]]; ;
            instant.descriptionText.text = allData.description[tempNum[i]];
            instant.uiType = settingUIName;
        }
    }
    //Delete Data Event
    public void ClearParent()
    {
        var contents = GameObject.FindGameObjectsWithTag("DataObj");
        foreach (var content in contents)
        {
            Destroy(content);
        }
    }
    #endregion

    //Click SearchType Dropdown
    public void SearhTypeDropdownClick(TMP_Dropdown tempDropdown)
    {
        for(int i = 0; i < searchType_Grp.Length; i++)
        {
            if(i == tempDropdown.value) { searchType_Grp[i].SetActive(true);}
            else { searchType_Grp[i].SetActive(false);}
        }
        searchTypeDropdown.value = tempDropdown.value;
    }

    #region Reset
    public void ClickReSet()
    {
        ClearParent();

        for(int i = 0; i < searchType_Grp.Length; i++)
        {
            searchType_Grp[i].SetActive(true);
        }
        CategoryReset();
        SearchBoxReset();

        searchTypeDropdown.value = 0;
        searchTypeDropdown.captionText.text = "Category";

        for(int i = 0; i < searchType_Grp.Length; i++)
        {
            if(i != searchTypeDropdown.value) { searchType_Grp[i].SetActive(false); }
        }
    }

    private void CategoryReset()
    {
        dropdown[0].captionText.text = "None";
        dropdown[0].value = 0;

        for(int i = 1; i < dropdown.Length; i++)
        {
            dropdown[i].ClearOptions();
            dropdown[i].value = 0;
            DataSettingAlgorism.DropDownOptionNone(dropdown[i]);
        }
    }

    private void SearchBoxReset()
    {
        searchBox.text = null;
        searchBoxTypeDropdown.value = 0;
        searchBoxTypeDropdown.captionText.text = "TagName";
    }
    #endregion

    //Text Split Event
    private void DevideString()
    {
        for (int i = 0; i < allData.tagName.Length; i++)
        {
            devidedStringList.Add(allData.tagName[i].Split('-'));
        }
    }
}
