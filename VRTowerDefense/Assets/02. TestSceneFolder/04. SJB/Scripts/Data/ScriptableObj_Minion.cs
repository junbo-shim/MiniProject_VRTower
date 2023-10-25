using UnityEngine;

public class ScriptableObj_Minion : ScriptableObject
{
    public int id;
    public string description;
    public string modelName;
    public int hp;
    public int damage;
    public float moveSpeed;
    public int agroArea;
    public int explosionArea;


    public ScriptableObj_Minion(int id_, string description_, string modelName_,
        int hp_, int damage_, float moveSpeed_, int agroArea_, int explosionArea_) 
    {
        id = id_;
        description = description_;
        modelName = modelName_;
        hp = hp_;
        damage = damage_;
        moveSpeed = moveSpeed_;
        agroArea = agroArea_;
        explosionArea = explosionArea_;
    }
}
