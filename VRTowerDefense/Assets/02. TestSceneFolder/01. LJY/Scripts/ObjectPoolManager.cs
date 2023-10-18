using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! ObjectPoolManager 클래스는 ObjectPooling 패턴을 활용하기 위해 제작한 클래스이다
public class ObjectPoolManager : MonoBehaviour
{
    // 싱글톤 사용
    public static ObjectPoolManager Instance;

    [SerializeField]    // 오브젝트 풀에 담을 프리팹
    private GameObject poolingObjectPrefab;

    // 오브젝트들을 담아놓을 자료구조 Queue 생성
    Queue<Bullet> poolingObjectQueue = new Queue<Bullet>();

    private void Awake()
    {
        Instance = this;
        Initialize(30);
    }

    //! 오브젝트를 큐에 initCount만큼 반복해서 넣어준다
    private void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    /**
     * @brief Bullet 오브젝트를 새로 생성하고 비활성화, 부모오브젝트 설정을 해주는 함수이다.
     * @return 생성 후 설정이 다 된 Bullet을 반환한다.
     */
    private Bullet CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    /**
     * @brief 큐에 저장된 Bullet 오브젝트를 꺼내오는 함수이다.
     * @return 큐에 있는 Bullet을 반환한다.
     */
    public static Bullet GetObject()
    {
        if(Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }   // if: 큐에 남은 오브젝트가 존재한다면
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }   // else: 큐에 남은 오브젝트가 존재하지 않는다면
    }

    //! 사용이 끝난 오브젝트를 큐로 넣어주는 함수이다.
    public static void ReturnObject(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        obj.transform.position = Vector3.zero;
        Instance.poolingObjectQueue.Enqueue(obj);
    }

}
