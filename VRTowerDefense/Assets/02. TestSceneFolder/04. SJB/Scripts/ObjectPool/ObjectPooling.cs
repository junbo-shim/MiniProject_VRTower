using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // Object Pooling 할 프리팹
    protected GameObject prefab;
    // Object Pool 을 보관해둘 보관함
    protected Transform poolHolder;
    // Pool Stack
    protected Stack<GameObject> poolStack;
    // Pool Size - 상속 받아서 크기 지정, 몇 번 push 를 해놓을 것인가
    protected int poolSize;


    protected virtual void MakePool()
    {
        // Stack 초기화
        poolStack = new Stack<GameObject>();

        // poolSize 만큼 반복하여 
        for (int i = 0; i < poolSize; i++) 
        {
            // 보관함 하위에서 프리팹 생성
            GameObject obj =
                Instantiate(prefab, Vector3.zero, Quaternion.identity, poolHolder);

            // 생성한 오브젝트 끄기
            obj.SetActive(false);
            // Pool Stack 에 추가
            poolStack.Push(obj);
        }
    }

    public GameObject GetPoolObject() 
    {
        // Pool 에 Object 가 있다면
        if (poolStack.Count > 0) 
        {
            // Pool Stack 에 있는 Object 꺼내기
            GameObject obj = poolStack.Pop();
            obj.SetActive(true);
            return obj;
        }
        // Pool 에 Object 가 없다면
        else 
        {
            // 새롭게 Object 를 생성하여 Stack 에 추가
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
