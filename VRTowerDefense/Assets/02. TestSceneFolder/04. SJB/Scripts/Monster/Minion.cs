using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonBase
{
    private Transform boss;
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;
    private Color defaultColor;

    private void OnEnable()
    {
        Init();
    }

    private void FixedUpdate()
    {
        base.Move();
    }

    private void OnCollisionStay(Collision collision)
    {
        // 추후에 name 에서 tag 또는 layer 로 변경
        if (collision.gameObject.name.Contains("Player")) 
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            Invoke("Attack", 1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 추후에 name 에서 tag 또는 layer 로 변경
        if (other.GetComponent<Bullet>() == true) 
        {
            this.GetHit(other, (int)other.GetComponent<Bullet>().bulletAtk);
        }
    }

    protected override void Init()
    {
        base.Init();

        defaultColor = gameObject.GetComponent<MeshRenderer>().material.color;
        boss = GameObject.Find("EarthGolem").transform;
        baseMinionPool = boss.GetComponent<Boss>().baseMinionPool;
        fastMinionPool = boss.GetComponent<Boss>().fastMinionPool;

        if (gameObject.name == "BaseMinion" + "(Clone)") 
        {
            // 수치 조정 필요
            this.healthPoint = 1 * 5;
            this.damage = 20;
            this.moveSpeed = 600f;
        }
        else 
        {
            // 수치 조정 필요
            this.healthPoint = 2 * 5;
            this.damage = 20;
            this.moveSpeed = 1200f;
        }
    }

    protected override void Attack()
    {
        GameManager.instance.HpMin(damage);
        CheckReturnPool(gameObject);
    }

    protected override void GetHit(Collider other, int damage)
    {
        healthPoint -= damage;

        if (healthPoint <= 0) 
        {
            CheckReturnPool(gameObject);
        }
    }

    private void CheckReturnPool(GameObject obj) 
    {
        if (obj.name == "BaseMinion" + "(Clone)") 
        {
            gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
            baseMinionPool.ReturnPoolObject(obj);
        }
        else 
        {
            gameObject.GetComponent<MeshRenderer>().material.color = defaultColor;
            fastMinionPool.ReturnPoolObject(obj);
        }
    }
}
