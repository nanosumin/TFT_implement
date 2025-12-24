using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UnitData
{
    public string id;
    public string name;
    public int cost;
    public float hp;
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float moveSpeed = 2f;
    public List<string> traits;
}

[System.Serializable]
public class UnitDataList
{
    public List<UnitData> units;
}