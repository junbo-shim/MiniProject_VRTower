using UnityEngine;

[System.Serializable]
public class BulletData
{
    public float damage;
    public float speed;
    public float slow;

    public string additionalData;
    public int debuffDuration;


    public BulletData(float damage_, float speed_, string additionalData_, float slow_ , int debuffDuration_)
    {
        damage = damage_;
        speed = speed_;
        slow = slow_;
        debuffDuration = debuffDuration_;
        additionalData = additionalData_;
        this.slow = slow;
    }
}