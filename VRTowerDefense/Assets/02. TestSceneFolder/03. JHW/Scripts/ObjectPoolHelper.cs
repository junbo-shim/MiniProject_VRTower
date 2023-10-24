using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolHelper : MonoBehaviour
{
    // Singleton 인스턴스
    public static ObjectPoolHelper Instance;

    // 오브젝트 풀 리스트
    public List<ObjectPool> objectPools = new List<ObjectPool>();

    private void Awake()
    {
        // Singleton 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복된 오브젝트 파괴
            return;
        }
    }

    public GameObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        ObjectPool pool = objectPools.Find(p => p.name == poolName);
        if (pool != null)
        {
            return pool.GetObject(position, rotation);
        }

        Debug.LogError("Object pool with name '" + poolName + "' not found.");
        return null;
    }

    public void ReturnObjectToPool(string poolName, GameObject obj)
    {
        ObjectPool pool = objectPools.Find(p =>  p.name == poolName && !p.gameObject.activeSelf);
        if (pool != null)
        {
          
             pool.ReturnObjectToPool(obj);        
        }
        else
        {
            Debug.LogError("Object pool with name '" + poolName + "' not found.");
        }
    }
}
