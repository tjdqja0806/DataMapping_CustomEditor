using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentData : MonoBehaviour
{
    public List<string> dataCategoryName;

    public List<string> symbolName;
    public List<string> description;

    public void ListClear()
    {
        symbolName.Clear();
        description.Clear();
    }

}
