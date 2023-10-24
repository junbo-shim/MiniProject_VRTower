using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Player_Table
{
    public int CSVID;
    public string Description;
    public int PlayerHp;
    public int Basic_Weapon;
    public int Reinforced_Weapon;
}




[CreateAssetMenu(fileName = "Player Data", menuName = "Player/Player Data")]
public class PlayerDataScriptableObject : ScriptableObject
{
    public List<Player_Table> items = new List<Player_Table>();
}


