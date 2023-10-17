using UnityEditor;
using UnityEngine;

public class FastMinionPool : ObjectPooling
{
    private void Awake()
    {
        SetProjectilePool();
    }

    private void SetProjectilePool()
    {
        this.prefab = (GameObject)Resources.Load("FastMinion");
        this.poolHolder = gameObject.transform;
        this.poolSize = 5;
        base.MakePool();
    }
}
