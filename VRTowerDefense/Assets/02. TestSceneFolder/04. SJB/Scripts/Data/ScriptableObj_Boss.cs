using UnityEngine;

public class ScriptableObj_Boss : ScriptableObject
{
    public int id;
    public string description;
    public int hp;
    public float moveSpeed;
    public float atkCoolTime;
    public int weakPointSize;
    public float weakPointMultiflier;
    public int weakPointDuration;


    public ScriptableObj_Boss (int id_, string description_, int hp_, float moveSpeed_,
        float atkCoolTime_, int weakPointSize_, float weakPointMultiflier_, 
        int weakPointDuration_) 
    {
        id = id_;
        description = description_;
        hp = hp_;
        moveSpeed = moveSpeed_;
        atkCoolTime = atkCoolTime_;
        weakPointSize = weakPointSize_;
        weakPointMultiflier = weakPointMultiflier_;
        weakPointDuration = weakPointDuration_;
    }
}
