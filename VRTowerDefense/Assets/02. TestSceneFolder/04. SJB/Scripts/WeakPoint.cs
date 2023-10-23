using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private Transform boss;
    private Vector3 normalSize;
    private Color normalColor;
    private WaitForSecondsRealtime weakTime;
    private WaitForSecondsRealtime inactiveTime;
    private bool isWeakOn = false;

    private void Awake()
    {
        boss = GameObject.Find("EarthGolem").transform;
        // 기본 사이즈와 컬러를 캐싱
        normalSize = transform.localScale;
        normalColor = gameObject.GetComponent<MeshRenderer>().material.color;
        weakTime = boss.GetComponent<Boss>().weakTime;
        inactiveTime = boss.GetComponent<Boss>().inactiveTime;
    }

    public void StartWeakRoutine() 
    {
        StartCoroutine(OnOffWeakPoint());
    }

    // 약점 활성비활성화 Coroutine
    public IEnumerator OnOffWeakPoint() 
    {
        isWeakOn = true;
        transform.localScale *= 2;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
        yield return weakTime;
        isWeakOn = false;
        transform.localScale = normalSize;
        gameObject.GetComponent<MeshRenderer>().material.color = normalColor;
    }

    // 약점에 맞을 경우
    private void OnTriggerEnter(Collider other)
    {
        // 닿은 Collider 가 총알이면
        if (other.GetComponent<Bullet>())
        {
            // 데미지 함수를 실행한다
            boss.GetComponent<Boss>().
                GetHitWeakPoint(other, (int)other.GetComponent<Bullet>().bulletAtk);

            if (isWeakOn == false) 
            {
                StartCoroutine(InactiveForSecond());
            }
            else if (isWeakOn == true) 
            {
                /* Do Nothing */
            }
        }
    }

    // 약점 히트 시 offon 기능
    private IEnumerator InactiveForSecond() 
    {
        gameObject.SetActive(false);
        yield return inactiveTime;
        gameObject.SetActive(true);
    }
}
