using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData
{
    public int CSVID;
    public string Description;
    public int UnitID;
    public int Price;
    public int TotalNum;
}

[CreateAssetMenu(fileName = "Item Data", menuName = "Shop/Item Data")]
public class ItemDataScriptableObject : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
}
