using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon_Table
{
    public int CSVID;
    public string Description;
    public int Firing_Interval;
    public float Projectile;
}



[CreateAssetMenu(fileName = "PlayerWeapon Data", menuName = "Player/PlayerWeapon Data")]
public class PlayerWeaponDataScriptableObject : ScriptableObject
{
    public List<Weapon_Table> items = new List<Weapon_Table>();
}
