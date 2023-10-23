using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonBase
{
    private Transform boss;
    private MeshRenderer mesh;
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;
    private WaitForSecondsRealtime flickerTime;
    private Color defaultColor;
    private bool isAttack = false;

    private void OnEnable()
    {
        Init();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 추후에 name 에서 tag 또는 layer 로 변경
        if (other.GetComponent<Bullet>()) 
        {
            StartCoroutine(ChangeColor(5));
            this.GetHit(other, (int)other.GetComponent<Bullet>().bulletAtk);
        }

        // 추후에 name 에서 tag 또는 layer 로 변경
        if (other.gameObject.name.Contains("Player"))
        {
            
        }
    }

    protected override void Init()
    {
        base.Init();

        mesh = transform.Find("Ani").GetComponent<MeshRenderer>();
        defaultColor = mesh.material.color;
        flickerTime = new WaitForSecondsRealtime(0.2f);
        boss = GameObject.Find("EarthGolem").transform;

        baseMinionPool = GameObject.Find("PoolControl").
            transform.Find("BaseMinion Pool").
            gameObject.GetComponent<BaseMinionPool>();

        fastMinionPool = GameObject.Find("PoolControl").
            transform.Find("FastMinion Pool").
            gameObject.GetComponent<FastMinionPool>();

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

    protected override void Move()
    {
        if (this.agent.isStopped == false) 
        {
            base.Move();
            transform.Find("Ani").transform.Rotate(transform.forward);
        }
        else if (this.agent.isStopped == true)
        {
            /* Do Nothing */
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

    private IEnumerator ChangeColor(int num) 
    {
        int time = default;

        while (time < num) 
        {
            mesh.material.color = Color.red;
            yield return flickerTime;
            mesh.material.color = defaultColor;
            yield return flickerTime;
            time += 1;
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
