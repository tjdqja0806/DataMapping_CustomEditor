using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFunction_Data : MonoBehaviour
{
    public string[] functionContent;

    private GameObject model;
    private List<Transform>[] funtionModel;
    private AllDataObject allDataManager;
    
    void Awake()
    {
        allDataManager = GameObject.Find("AllDataManager(Clone)").GetComponent<AllDataObject>();
        model = GameObject.Find("TestModel");
    }

    void Start()
    {
        funtionModel = new List<Transform>[functionContent.Length];

        FindFunctionModelFromAllObject(model);

        AddFuntion_Data(funtionModel, functionContent);
    }
    void Update()
    {
        
    }

    private void FindFunctionModelFromAllObject(GameObject parentObj)
    {
        Transform[] childObj = parentObj.transform.GetComponentsInChildren<Transform>();
        List<Transform> funtionChild = new List<Transform>();

        for(int i = 0; i < functionContent.Length; i++)
        {
            funtionModel[i] = new List<Transform>();
            foreach(Transform child in childObj)
            {
                if (child.name.Contains(functionContent[i]))
                {
                    funtionModel[i].Add(child);
                }
            }
            funtionChild.Clear();
        }
    }

    private void AddFuntion_Data(List<Transform>[] model, string[] functionText)
    {
        for (int i = 0; i < functionText.Length; i++)
        {
            for (int j = 0; j < model[i].Count; j++)
            {
                switch (i)
                {
                    case 0:
                        //1��° ��� Or Data �߰��� �ʿ��� ���
                        break;
                    case 1:
                        //2��° ��� Or Data �߰��� �ʿ��� ���
                        break;
                    case 2:
                        //3��° ��� Or Data �߰��� �ʿ��� ���
                        break;
                    case 3:
                        //4��° ��� Or Data �߰��� �ʿ��� ���
                        break;
                    case 4:
                        //5��° ��� Or Data �߰��� �ʿ��� ���
                        break;
                    case 5:
                        //6��° ��� Or Data �߰��� �ʿ��� ���
                        break;
                }
            }
        }
    }
}
