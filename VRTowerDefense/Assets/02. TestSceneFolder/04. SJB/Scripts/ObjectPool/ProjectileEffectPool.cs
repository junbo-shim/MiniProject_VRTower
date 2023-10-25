using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectPool : ObjectPooling
{
    private void Awake()
    {
        SetProjectileEffectPool();
    }

    private void SetProjectileEffectPool()
    {
        this.prefab = (GameObject)Resources.Load("BigExplosion");
        this.poolHolder = gameObject.transform;
        this.poolSize = 5;
        base.MakePool();
    }
}
