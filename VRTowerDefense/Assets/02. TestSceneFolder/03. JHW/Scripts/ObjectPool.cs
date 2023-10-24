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
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        Boss boss = FindObjectOfType<Boss>();

        foreach (var obj in objects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                float distance = Vector3.Distance(transform.position, boss.transform.position);
                Vector3 newRotation = new Vector3(90.0f- Normalization(distance), rotation.eulerAngles.y, rotation.eulerAngles.z);

                obj.transform.rotation = Quaternion.Euler(newRotation);

                //obj.transform.rotation = rotation;

                obj.SetActive(true);

                return obj;
            }
        }

        return null;
    }
    public float Normalization(float value)
    {
        float dasd = 0;
        dasd =((value - 0) / (332 - 0))*5;
        dasd = 10 - dasd;

        return dasd;
    }
    public void ReturnObjectToPool(GameObject obj)
    {
       
        obj.SetActive(false);
    }
}