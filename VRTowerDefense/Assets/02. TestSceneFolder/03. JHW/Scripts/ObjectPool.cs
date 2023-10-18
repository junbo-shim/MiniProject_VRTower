using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    [SerializeField]
    public List<GameObject> objects;

    private void Awake()
    {
        objects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            // Load the prefab from the 'prefab' variable and instantiate it.
            GameObject obj = Instantiate(prefab);

            // Set the parent to this ObjectPool object.
            obj.transform.SetParent(transform);

            obj.transform.rotation = Quaternion.Euler(90, 0, 0);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        foreach (var obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
       
        obj.SetActive(false);
    }
}