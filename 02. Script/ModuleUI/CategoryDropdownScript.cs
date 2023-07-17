using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryDropdownScript : MonoBehaviour
{
    [HideInInspector] public int dropdownNum;//생성된 Dropdown 순서
    [HideInInspector] public string seletedOption; //선택될 Dropdown Option Text
    [HideInInspector] public CategoryDropdownScript postDropdown; //전 CategoryDropdown
    [HideInInspector] public AllDataModule allDataModule;
    [HideInInspector] public List<int> nextCategotyInt = new List<int>();

    private TMP_Dropdown curDropdown;
    private List<string> dropdownOptions = new List<string>();

    void Awake()
    {
        curDropdown = GetComponent<TMP_Dropdown>();
        allDataModule = GameObject.Find("DataSeleteUI (1)").GetComponent<AllDataModule>();
    }

    void OnEnable()
    {
        CategoryReset();
        AddDropdownOptions();
    }

    public void AddDropdownOptions()
    {
        dropdownOptions.Clear();
        nextCategotyInt.Clear();
        if (dropdownNum == 0)
        {
            for (int i = 0; i < allDataModule.categoryList.Count; i++)
            {
                dropdownOptions.Add(allDataModule.categoryList[i][dropdownNum]);
                nextCategotyInt.Add(i);
            }
            DataSettingAlgorism.DropDownOptionInsert(curDropdown, dropdownOptions);
        }
        else if (postDropdown.seletedOption != "None")
        {
            for (int i = 0; i < postDropdown.nextCategotyInt.Count; i++)
            {
                if (allDataModule.categoryList[postDropdown.nextCategotyInt[i]][dropdownNum - 1].Contains(postDropdown.seletedOption))
                {
                    dropdownOptions.Add(allDataModule.categoryList[postDropdown.nextCategotyInt[i]][dropdownNum]);
                    nextCategotyInt.Add(postDropdown.nextCategotyInt[i]);
                }
            }
            DataSettingAlgorism.DropDownOptionInsert(curDropdown, dropdownOptions);
        }
        else
        {
            DataSettingAlgorism.DropDownOptionNone(curDropdown);
        }
    }

    public void DropdownOptionClick()
    {
        seletedOption = curDropdown.options[curDropdown.value].text;
        allDataModule.clickedCategoryNum = dropdownNum;
        allDataModule.CategoryClick();
    }

    public void CategoryReset()
    {
        if (dropdownNum == 0)
        {
            seletedOption = "None";
            DataSettingAlgorism.DropdownValueReset(curDropdown);
        }
        else
        {
            curDropdown.ClearOptions();
            curDropdown.captionText.text = "None";
            DataSettingAlgorism.DropDownOptionNone(curDropdown);
            curDropdown.value = 0;
        }
    }
}
