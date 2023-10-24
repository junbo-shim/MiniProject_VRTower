using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ProjectileData
{
    public int CSVID;
    public string Description;
    public string Model;
    public int Atk;
    public float Movement_Speed;
    public int Debuff;
    public int projectile_Speed;
    public int LifeTime;
}


[CreateAssetMenu(fileName = "Tower Data", menuName = "Tower/ProjectileData Data")]
public class TowerDataScriptableObject : ScriptableObject
{
    public List<ProjectileData> items = new List<ProjectileData>();
}

