using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Projectile_Table
{
    public int CSVID;
    public string Description;
    public string Model;
    public float Atk;
    public int Critical_Percentage;
    public float Critical_Rate;
    public int Crash;
    public int LifeTime;
}

[CreateAssetMenu(fileName = "PlayerProjectile Data", menuName = "Player/PlayerProjectile Data")]
public class PlayerProjectileDataScriptableObject : ScriptableObject
{
    public List<Projectile_Table> items = new List<Projectile_Table>();
}
