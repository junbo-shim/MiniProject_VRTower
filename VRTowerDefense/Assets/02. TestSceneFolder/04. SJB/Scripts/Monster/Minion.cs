using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonBase
{
    private Transform boss;
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;

    private void OnEnable()
    {
        Init();
    }

    private void FixedUpdate()
    {
        base.Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 추후에 name 에서 tag 또는 layer 로 변경
        if (collision.gameObject.name == "Player") 
        {
            Attack();
            CheckReturnPool(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 추후에 name 에서 tag 또는 layer 로 변경
        if (other.name == "Bullet") 
        {
            //GetHit();

        }
    }

    protected override void Init()
    {
        base.Init();

        boss = GameObject.Find("EarthGolem").transform;
        baseMinionPool = boss.GetComponent<Boss>().baseMinionPool;
        fastMinionPool = boss.GetComponent<Boss>().fastMinionPool;

        this.rigidGravity = -1000f;

        if (gameObject.name == "BaseMinion" + "(Clone)") 
        {
            this.healthPoint = 1;
            this.damage = 20;
            this.moveSpeed = 600f;
        }
        else 
        {
            this.healthPoint = 2;
            this.damage = 20;
            this.moveSpeed = 1200f;
        }
    }

    protected override void Attack()
    {
        
    }

    protected override void GetHit(Collider other, int damage)
    {
        //base.GetHit();

    }

    private void CheckReturnPool(GameObject obj) 
    {
        if (obj.name == "BaseMinion" + "(Clone)") 
        {
            baseMinionPool.ReturnPoolObject(obj);
        }
        else 
        {
            fastMinionPool.ReturnPoolObject(obj);
        }
    }
}
