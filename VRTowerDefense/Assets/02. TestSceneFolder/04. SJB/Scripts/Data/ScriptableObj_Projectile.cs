using UnityEngine;

public class ScriptableObj_Projectile : ScriptableObject
{
    public int id;
    public string description;
    public string modelName;
    public int hp;
    public float minSpeed;
    public float maxSpeed;
    public int damage;
    public float coolTime;
    public int respawnNumber;


    public ScriptableObj_Projectile(int id_, string description_, string modelName_,
        int hp_, float minSpeed_, float maxSpeed_, int damage_, float coolTime_,
        int respawnNumber_) 
    {
        id = id_;
        description = description_;
        modelName = modelName_;
        hp = hp_;
        minSpeed = minSpeed_;
        maxSpeed = maxSpeed_;
        damage = damage_;
        coolTime = coolTime_;
        respawnNumber = respawnNumber_;
    }
}
