using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartScriptableObjLoad : MonoBehaviour
{
    [HideInInspector]
    public ChartScriptableObj chartData;

    private string chartName;
    private RectTransform chartParent;
    // Start is called before the first frame update
    void Awake()
    {
        chartName = gameObject.name.Replace("_Grp", "");
        chartData = Resources.Load<ChartScriptableObj>("Chart/" + chartName + "Data");
        chartParent = GetComponent<RectTransform>();
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        for(int i =0; i < chartData.chartCount; i++)
        {
            var chart = Instantiate(Resources.Load<GameObject>("Chart/" + chartName));
            chart.name = chartName + " " + (i+1);
            chart.transform.SetParent(chartParent, false);
            //chartPrefab의 Script에 데이터 추가하는 부분 
        }
    }

    public void AddInit()
    {
        chartData.chartCount += 1;
        //ScriptableObject의 심볼네임, 테그네임 추가
        var chart = Instantiate(Resources.Load<GameObject>("Chart/" + chartName));
        chart.name = chartName + " "  + chartData.chartCount;
        chart.transform.SetParent(chartParent, false);
    }

}
