using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllDataModule : MonoBehaviour
{
    [Header("Category")]
    public CategoryDropdownScript categoryPrefab;
    public RectTransform categoryParent;
    public TextMeshProUGUI curCategoryCountText;
    [HideInInspector] public int clickedCategoryNum = 0; 
    [HideInInspector] public List<string[]> categoryList = new List<string[]>();

    [Space]
    [Header("SearchBox")]
    public SearchBoxScript searchBoxScript;

    [Space]
    [Header("SearcType")]
    public TMP_Dropdown searchTypeDropdown;
    public GameObject[] searchType_Grp;


    [Space]
    [Header("DataObject")]
    public AllDataPrefab dataPrefab;
    public RectTransform dataParents;

    private int maxCategoryCount = 0;
    private int curCategoryCount = 0;
    private List<CategoryDropdownScript> categoryScript = new List<CategoryDropdownScript>();

    private AllDataObject allData;
    
    private string settingUIName;
    private bool isSetting;

    void Start()
    {
        allData = GameObject.Find("AllDataManager(Clone)").GetComponent<AllDataObject>();
        DevideString();
        CategoryInstantiate();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        ModuleReset();
    }

    #region Categorty
    //Instantiate Category Dropdown
    private void CategoryInstantiate()
    {
        for(int i = 0; i < maxCategoryCount; i++)
        {
            CategoryDropdownScript instant = Instantiate(categoryPrefab);
            instant.transform.SetParent(categoryParent, false);
            instant.gameObject.name = "Category" + (i + 1);
            instant.dropdownNum = i;
            //instant.allDataModule = this;
            if (i != 0) 
            { 
                instant.postDropdown = categoryScript[i - 1].GetComponent<CategoryDropdownScript>();
                instant.gameObject.SetActive(false);
            }
            categoryScript.Add(instant);
        }
    }

    private void CategoryActivation()
    {
        for(int i = 0; i < maxCategoryCount; i++)
        {
            if(i <= curCategoryCount) { categoryScript[i].gameObject.SetActive(true); }
            else { categoryScript[i].gameObject.SetActive(false); }
        }
    }

    //Category UpDown Button Click Event
    public void CategoryCountUp(bool countUp)
    {
        if (curCategoryCount < maxCategoryCount && countUp)  { curCategoryCount += 1; }
        else if (curCategoryCount > 0 && !countUp) { curCategoryCount -= 1; }
        curCategoryCountText.text = (curCategoryCount + 1).ToString();
        CategoryActivation();
    }

    public void CategoryClick()
    {
        for(int i = clickedCategoryNum + 1; i <= curCategoryCount; i++)
        {
            categoryScript[i].seletedOption = "None";
            categoryScript[i].AddDropdownOptions();
        }
    }

    public void CategorySearch()
    {
        List<int> tempInt = new List<int>();
        tempInt = categoryScript[clickedCategoryNum].nextCategotyInt;
        Debug.Log(categoryScript[clickedCategoryNum].seletedOption);
        for (int i = 0; i < tempInt.Count; i++)
        {
            if (categoryList[tempInt[i]][clickedCategoryNum].Contains(categoryScript[clickedCategoryNum].seletedOption))
            {
                DataInstantiate(tempInt[i]);
            }
        }
    }

    #endregion

    #region SearchBox
    
    public void SearchBoxSearch()
    {
        List<int> searchNum = new List<int>();
        searchNum.Clear();
        switch (searchBoxScript.searchBoxTypeDropdown.value)
        {
            case 0:
                searchNum = DataSettingAlgorism.SearchBoxSearch(searchBoxScript.searchBox.text, allData.tagName);
                break;
            case 1:
                searchNum = DataSettingAlgorism.SearchBoxSearch(searchBoxScript.searchBox.text, allData.description);
                break;
        }

        for(int i = 0; i < searchNum.Count; i++)
        {
            DataInstantiate(searchNum[i]);
        }
    }

    #endregion

    public void Search()
    {
        ClearDataScrollView();
        if ((searchTypeDropdown.value == 0 && categoryScript[0].seletedOption == "None")
            || (searchTypeDropdown.value == 1 && searchBoxScript.searchBox.text == null))
            AllSearch();
        else
        {
            switch (searchTypeDropdown.value)
            {
                case 0:
                    CategorySearch();
                    break;
                case 1:
                    SearchBoxSearch();
                    break;
            }
        }
    }

    //Data Prefab Instantiate
    private void DataInstantiate(int num)
    {
        AllDataPrefab instant = Instantiate(dataPrefab);
        instant.transform.SetParent(dataParents, false);
        instant.gameObject.name = allData.tagName[num];
        instant.tagName = allData.tagName[num];
        instant.description = allData.description[num];
        instant.tagNameText.text = allData.tagName[num]; ;
        instant.descriptionText.text = allData.description[num];
        instant.uiType = settingUIName;
    }

    private void AllSearch()
    {
        for (int i = 0; i < allData.tagName.Length; i++)
        {
            DataInstantiate(i);
        }
    }

    //Clear Data ScrollView
    private void ClearDataScrollView()
    {
        var contents = GameObject.FindGameObjectsWithTag("DataObj");
        foreach(var content in contents)
        {
            Destroy(content);
        }
    }

    //Click SearchType Dropdown
    public void SearhTypeDropdownClick(TMP_Dropdown tempDropdown)
    {
        for (int i = 0; i < searchType_Grp.Length; i++)
        {
            if (i == tempDropdown.value) { searchType_Grp[i].SetActive(true); }
            else { searchType_Grp[i].SetActive(false); }
        }
        searchTypeDropdown.value = tempDropdown.value;
    }

    //Reset All Module
    public void ModuleReset()
    {
        ClearDataScrollView();
        DataSettingAlgorism.DropdownValueReset(searchTypeDropdown);
        for(int i = 0; i < searchType_Grp.Length; i++) { searchType_Grp[i].SetActive(true); }
        curCategoryCount = 0;
        curCategoryCountText.text = (curCategoryCount + 1).ToString();
        for (int i = 0; i < categoryScript.Count; i++)
        {
            if (i == 0) { categoryScript[0].CategoryReset(); }
            else { categoryScript[i].gameObject.SetActive(false); }
        }
        searchBoxScript.SearchBoxReset();
        SearhTypeDropdownClick(searchTypeDropdown);
    }

    public void UISettingClick(string uiName)
    {
        isSetting = !isSetting;
        gameObject.SetActive(isSetting);
        settingUIName = uiName;
    }
    private void DevideString() //Catoegory Devied & Set MaxCategory
    {
        for (int i = 0; i < allData.tagName.Length; i++)
        {
            categoryList.Add(allData.tagName[i].Split('-'));

            if(i == 0) { maxCategoryCount = allData.tagName[i].Split('-').Length; }
        }
    }
}
