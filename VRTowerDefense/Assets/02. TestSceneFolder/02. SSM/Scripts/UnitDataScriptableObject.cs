using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData
{
    public int CSVID;
    public string Description;
    public string Type;
    public int Duration;
    public float Firing_Interval;
    public string Recognition;

}

[CreateAssetMenu(fileName = "Unit Data", menuName = "Tower/UnitData Data")]
public class UnitDataScriptableObject : ScriptableObject
{
    public List<UnitData> items = new List<UnitData>();
}
