using UnityEngine;

public class PoolControl : MonoBehaviour
{
    private void Awake()
    {
        CreateProjectilePool();
        CreateBaseMinionPool();
        CreateFastMinionPool();
        CreateMinionSpawnPool();
    }

    private void CreateProjectilePool()
    {
        GameObject obj = new GameObject("Projectile Pool");
        obj.AddComponent<ProjectilePool>();
        obj.transform.SetParent(gameObject.transform, true);
    }

    private void CreateBaseMinionPool() 
    {
        GameObject obj = new GameObject("BaseMinion Pool");
        obj.AddComponent<BaseMinionPool>();
        obj.transform.SetParent(gameObject.transform, true);
    }

    private void CreateFastMinionPool()
    {
        GameObject obj = new GameObject("FastMinion Pool");
        obj.AddComponent<FastMinionPool>();
        obj.transform.SetParent(gameObject.transform, true);
    }

    private void CreateMinionSpawnPool() 
    {
        GameObject obj = new GameObject("SpawnEffect Pool");
        obj.AddComponent<SpawnEffectPool>();
        obj.transform.SetParent(gameObject.transform, true);
    }
}
