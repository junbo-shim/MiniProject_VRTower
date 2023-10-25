
using UnityEngine;

public class Minion : MonBase
{
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;
    private MinionEffectPool minionEffectPool;


    private int distanceOfPlayer;
    private int attackRange = 5;
    private int explosionRange = 10;


    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        agent.isStopped = false;
    }

    private void Start()
    {
        minionEffectPool = transform.parent.parent.Find("MinionEffect Pool").GetComponent<MinionEffectPool>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>()) 
        {
            this.GetHit(other, (int)other.GetComponent<Bullet>().bulletAtk);
            GameObject effect = minionEffectPool.GetPoolObject();
            effect.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            CheckReturnPool(gameObject);
        }

        if (other.name == "Player") 
        {
            this.agent.isStopped = true;
            Attack();
            GameObject effect = minionEffectPool.GetPoolObject();
            effect.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            CheckReturnPool(gameObject);
        }
    }

    protected override void Init()
    {
        base.Init();

        baseMinionPool = transform.parent.GetComponent<BaseMinionPool>();
        fastMinionPool = transform.parent.GetComponent<FastMinionPool>();  

        if (gameObject.name == "BaseMinion" + "(Clone)") 
        {
            // 수치 조정 필요
            this.healthPoint = 1 * 5;
            this.damage = 20;
        }
        else 
        {
            // 수치 조정 필요
            this.healthPoint = 2 * 5;
            this.damage = 20;
        }
    }

    protected override void Move()
    {
        distanceOfPlayer = (int)Vector3.Distance(transform.position, player.position);

        if (this.agent.isStopped == false) 
        {
            base.Move();

            //if (distanceOfPlayer > attackRange)
            //{
                if (gameObject.name.Contains("Base")) 
                {
                    transform.Find("Ani").transform.Rotate(transform.right * 5f, Space.World);
                }
                else if (gameObject.name.Contains("Fast")) 
                {
                    transform.Find("Ani").transform.Rotate(transform.right * 10f, Space.World);
                }
            //}
            //else if (distanceOfPlayer <= attackRange)
            //{
            //    this.agent.isStopped = true;
            //}
        }
    }

    protected override void Attack()
    {
        Debug.LogWarning("폭탄 딜 : " + damage);
        GameManager.instance.HpMin(damage);
    }

    protected override void GetHit(Collider other, int damage_)
    {
        healthPoint -= damage_;

        if (healthPoint <= 0) 
        {
            if (distanceOfPlayer <= explosionRange)
            {
                GameManager.instance.HpMin(damage);
            }
            else if (distanceOfPlayer > explosionRange)
            {
                /* Do Nothing */
            }
        }
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
