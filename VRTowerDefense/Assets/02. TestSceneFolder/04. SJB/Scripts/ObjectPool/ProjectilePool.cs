using UnityEditor;
using UnityEngine;

public class ProjectilePool : ObjectPooling
{
    private void Awake()
    {
        SetProjectilePool();
    }

    private void SetProjectilePool()
    {
        this.prefab = (GameObject)Resources.Load("Projectile");
        this.poolHolder = gameObject.transform;
        this.poolSize = 5;
        base.MakePool();
    }
}
