using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_Class_EntityType
{
    public string name;
    public int spawningChance;
    public int[] percentageRange;

    public DF_Class_EntityType (string _name, int _spawningChance)
    {
        name = _name;
        spawningChance = _spawningChance;
    }
}
