using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // Object Pooling �� ������
    protected GameObject prefab;
    // Object Pool �� �����ص� ������
    protected Transform poolHolder;
    // Pool Stack
    protected Stack<GameObject> poolStack;
    // Pool Size - ��� �޾Ƽ� ũ�� ����, �� �� push �� �س��� ���ΰ�
    protected int poolSize;


    protected virtual void MakePool()
    {
        // Stack �ʱ�ȭ
        poolStack = new Stack<GameObject>();

        // poolSize ��ŭ �ݺ��Ͽ� 
        for (int i = 0; i < poolSize; i++) 
        {
            // ������ �������� ������ ����
            GameObject obj =
                Instantiate(prefab, Vector3.zero, Quaternion.identity, poolHolder);
            // ������ ������Ʈ ����
            obj.SetActive(false);
            // Pool Stack �� �߰�
            poolStack.Push(obj);
        }
    }

    public GameObject GetPoolObject() 
    {
        // Pool �� Object �� �ִٸ�
        if (poolStack.Count > 0) 
        {
            // Pool Stack �� �ִ� Object ������
            GameObject obj = poolStack.Pop();
            obj.SetActive(true);
            return obj;
        }
        // Pool �� Object �� ���ٸ�
        else 
        {
            // ���Ӱ� Object �� �����Ͽ� Stack �� �߰�
            GameObject obj =
                Instantiate(prefab, Vector3.zero, Quaternion.identity, poolHolder);
            poolStack.Push(obj);
            return obj;
        }
    }

    public void ReturnPoolObject(GameObject obj) 
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolHolder, true);
        poolStack.Push(obj);
    }

}
