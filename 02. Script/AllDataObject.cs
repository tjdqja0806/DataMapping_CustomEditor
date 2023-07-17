using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDataObject : MonoBehaviour
{
    public string[] tagName;
    public string[] description;

    public List<Form> forms;

    public struct Form
    {
        public string plant;
        public string system;
        public string component;
        public string tag;
        public string description;
        public string unit;
        public string limitLo;
        public string limitHi;
    }
}
