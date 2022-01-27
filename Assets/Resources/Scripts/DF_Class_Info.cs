using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DF_Class_Info
{
    public int id;
    public string name, info;
}

[System.Serializable]
public class DF_Class_InfoList
{
    public DF_Class_Info[] infoList;
}
