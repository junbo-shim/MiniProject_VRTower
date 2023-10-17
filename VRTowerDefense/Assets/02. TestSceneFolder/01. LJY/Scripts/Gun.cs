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

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.currentState != playerController.battleState)
        {
            return;
        }
        attackAfter += Time.deltaTime;
        
        if(attackAfter > attackSpeed && ARAVR_Input.Get(ARAVR_Input.Button.IndexTrigger) /*&& !isAttacking*/)
        {
            attackAfter = 0f;
            var bullet = ObjectPoolManager.GetObject();
            Vector3 direction = ARAVR_Input.RHandDirection;
            bullet.transform.position = ARAVR_Input.RHandPosition;
            bullet.Shoot(direction);
            //StartCoroutine(ShootBullet());
        }

        if(ARAVR_Input.GetUp(ARAVR_Input.Button.IndexTrigger))
        {
            isAttacking = false;
        }
    }
    #region Legacy Coroutine
    //private IEnumerator ShootBullet()
    //{
    //    isAttacking = true;

    //    while(isAttacking)
    //    {
    //        var bullet = ObjectPoolManager.GetObject();
    //        Vector3 direction = ARAVR_Input.RHandDirection;
    //        bullet.transform.position = ARAVR_Input.RHandPosition;
    //        bullet.Shoot(direction);

    //        yield return new WaitForSeconds(1.0f / attackSpeed);
    //    }
    //}
    #endregion
}
