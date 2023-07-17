using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DataSettingAlgorism
{

    public static List<string[]> StringDevide(string[] text)
    {
        List <string[]> devidedText = new List<string[]>();

        for(int i = 0; i < text.Length; i++)
        {
            devidedText.Add(text[i].ToLower().Split('-'));
        }

        return devidedText;
    }
    
    public static List<string[]> StringListDevide(List<string> text)
    {
        List <string[]> devidedText = new List<string[]>();
        for (int i = 0; i < text.Count; i++)
        {
            devidedText.Add(text[i].ToLower().Trim().Split('-'));
        }
        return devidedText;
    }
    
    public static List<int> IntersectList(List<int> list1, List<int> list2)//List 교집합
    {
        List<int> resultList = new List<int>();

        var result = list1.Intersect(list2).ToList();
        resultList = result;

        return resultList;
    }
    public static void DropDownOptionInsert(TMP_Dropdown curDropdown, List<string> curDropdownString)//DropDown Option 추가
    {
        curDropdownString = curDropdownString.Distinct().ToList();

        curDropdown.options.Clear();
        if (curDropdownString[0] != "None")
            curDropdownString.Insert(0, "None");

        if(curDropdownString.Count > 1)
        {
            for (int i = 0; i < curDropdownString.Count; i++)
            {
                TMP_Dropdown.OptionData options = new TMP_Dropdown.OptionData();
                options.text = curDropdownString[i];
                curDropdown.options.Add(options);
            }
        }
        //DropdownValueReset(curDropdown);
    }
    public static void DropDownOptionNone(TMP_Dropdown curDropdown)//DropDown Option 추가
    {
        curDropdown.options.Clear();

        TMP_Dropdown.OptionData options = new TMP_Dropdown.OptionData();
        options.text = "None";
        curDropdown.options.Add(options);
        curDropdown.value = 0;
        //DropdownValueReset(curDropdown);
    }

    public static void DropdownValueReset(TMP_Dropdown curDropdown)
    {
        curDropdown.value = 0;
        curDropdown.Select();
        curDropdown.RefreshShownValue();
    }
    public static List<int> SearchBoxSearch(string searchText, string[] searchTarget)
    {
        List<int> tempNum = new List<int>();
        for(int i = 0; i < searchTarget.Length; i++)
        {
            if (searchTarget[i].ToLower().Contains(searchText.ToLower()))
            {
                tempNum.Add(i);
            }
        }
        return tempNum;
    }

}
