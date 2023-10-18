using UnityEngine;

[System.Serializable]
public class BulletData
{
    public float damage;
    public float speed;
    public float slow;
    public string additionalData;


    public BulletData(float _damage, float _speed, string _additionalData, float _slow)
    {
        damage = _damage;
        speed = _speed;
        slow = _slow;

        additionalData = _additionalData;
        this.slow = slow;
    }
}