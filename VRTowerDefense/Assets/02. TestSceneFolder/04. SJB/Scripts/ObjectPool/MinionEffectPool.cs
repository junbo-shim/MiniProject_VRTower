using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEffectPool : ObjectPooling
{
    private void Awake()
    {
        SetMinionEffectPool();
    }

    private void SetMinionEffectPool()
    {
        this.prefab = (GameObject)Resources.Load("SmallExplosion");
        this.poolHolder = gameObject.transform;
        this.poolSize = 5;
        base.MakePool();
    }
}
