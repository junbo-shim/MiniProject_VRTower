using UnityEditor;
using UnityEngine;

public class SpawnEffectPool : ObjectPooling
{
    private void Awake()
    {
        SetProjectilePool();
    }

    private void SetProjectilePool()
    {
        this.prefab = (GameObject)Resources.Load("SpawnEffect");
        this.poolHolder = gameObject.transform;
        this.poolSize = 5;
        base.MakePool();
    }
}
