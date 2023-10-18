using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private Transform boss;
    private Vector3 normalSize;
    private Color normalColor;
    private WaitForSecondsRealtime weakTime;

    private void Awake()
    {
        boss = GameObject.Find("EarthGolem").transform;
        // 기본 사이즈와 컬러를 캐싱
        normalSize = transform.localScale;
        normalColor = gameObject.GetComponent<MeshRenderer>().material.color;
        weakTime = boss.GetComponent<Boss>().weakTime;
    }

    public void MakeBossWeak() 
    {
        StartCoroutine(WeakenEnemy());
    }

    // 약점 활성화 Coroutine
    private IEnumerator WeakenEnemy() 
    {
        MakeSizeBig();
        yield return weakTime;
        ReturnNormal();
        yield break;
    }

    // 약점 확대
    public void MakeSizeBig() 
    {
        transform.localScale *= 2;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red; 
    }
    // 기본 상태로 복귀
    public void ReturnNormal() 
    {
        transform.localScale = normalSize;
        gameObject.GetComponent<MeshRenderer>().material.color = normalColor;
    }
    // 약점에 맞을 경우
    private void OnTriggerStay(Collider other)
    {
        // 닿은 Collider 가 총알이면
        if (other.GetComponent<Bullet>() == true)
        {
            // 데미지 함수를 실행한다
            boss.GetComponent<Boss>().
                GetHitWeakPoint(other, (int)other.GetComponent<Bullet>().bulletAtk);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
        }

        if (other.GetComponent<TurretShoot>() == true) 
        {
            // 데미지 함수를 실행한다
            boss.GetComponent<Boss>().
                GetHitWeakPoint(other, (int)other.GetComponent<TurretShoot>().bulletData.damage);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
        }
    }
}
