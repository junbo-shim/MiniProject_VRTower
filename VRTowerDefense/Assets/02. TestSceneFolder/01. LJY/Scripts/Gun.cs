using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject basicBulletPrefab;    // 기본 총알 프리팹
    private float attackSpeed = 1.0f;        // 공격 속도
    private float attackAfter = default;    // 공격 후 흐른 시간

    public bool isAttackable =  false;      // 공격 가능한 상태인지
    private bool isAttacking = false;       // 공격중인지 확인

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ARAVR_Input.Get(ARAVR_Input.Button.IndexTrigger) && !isAttacking)
        {
            StartCoroutine(ShootBullet());
        }

        if(ARAVR_Input.GetUp(ARAVR_Input.Button.IndexTrigger))
        {
            isAttacking = false;
        }
    }

    private IEnumerator ShootBullet()
    {
        isAttacking = true;

        while(isAttacking)
        {
            
            yield return new WaitForSeconds(1.0f / attackSpeed);
        }
    }
}
