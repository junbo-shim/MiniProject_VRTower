using UnityEditor;
using UnityEngine;

public class BaseMinionPool : ObjectPooling
{
    private void Awake()
    {
        SetProjectilePool();
    }

    private void SetProjectilePool()
    {
        this.prefab = (GameObject)Resources.Load("BaseMinion");
        this.poolHolder = gameObject.transform;
        this.poolSize = 5;
        base.MakePool();
    }
}
