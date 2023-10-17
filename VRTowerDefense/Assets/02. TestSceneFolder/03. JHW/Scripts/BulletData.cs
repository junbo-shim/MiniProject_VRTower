using UnityEngine;

[System.Serializable]
public class BulletData
{
    public float damage;
    public float speed;
    public string additionalData;

    public BulletData(float _damage, float _speed, string _additionalData)
    {
        damage = _damage;
        speed = _speed;
        additionalData = _additionalData;
    }
}