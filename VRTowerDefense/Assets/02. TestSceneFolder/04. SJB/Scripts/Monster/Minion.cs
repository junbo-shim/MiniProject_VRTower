using UnityEngine;

public class Minion : MonBase
{
    // Scriptable 
    private ScriptableObj_Minion baseMinionData;
    private ScriptableObj_Minion fastMinionData;
    // 오브젝트 풀
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;
    private MinionEffectPool minionEffectPool;
    // 졸개 속성
    private int distanceOfPlayer;
    private int agroRange;
    private int explosionRange;


    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        if (this.agent.isStopped == true) 
        {
            if (gameObject.name == "BaseMinion" + "(Clone)")
            {
                this.agent.speed = baseMinionData.moveSpeed;
            }
            else 
            {
                this.agent.speed = fastMinionData.moveSpeed;
            }
        }
    }

    private void Start()
    {
        baseMinionPool = transform.parent.GetComponent<BaseMinionPool>();
        fastMinionPool = transform.parent.GetComponent<FastMinionPool>();
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
            this.agent.speed = 0f;
            Attack();
            GameObject effect = minionEffectPool.GetPoolObject();
            effect.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            CheckReturnPool(gameObject);
        }
    }

    // 초기화
    protected override void Init()
    {
        base.Init();

        // Scriptable 할당
        baseMinionData = (ScriptableObj_Minion)CSVConverter_SJB.Instance.ScriptableObjDictionary[3001];
        fastMinionData = (ScriptableObj_Minion)CSVConverter_SJB.Instance.ScriptableObjDictionary[3002];

        // 만약 기본 졸개면
        if (gameObject.name == "BaseMinion" + "(Clone)") 
        {
            this.healthPoint = baseMinionData.hp;
            this.damage = baseMinionData.damage;
            agroRange = baseMinionData.agroArea;
            explosionRange = baseMinionData.explosionArea;
            this.agent.speed = baseMinionData.moveSpeed;
        }
        // 만약 빠른 졸개면
        else 
        {
            this.healthPoint = fastMinionData.hp;
            this.damage = fastMinionData.damage;
            agroRange = fastMinionData.agroArea;
            explosionRange = fastMinionData.explosionArea;
            this.agent.speed = fastMinionData.moveSpeed;
        }
    }

    protected override void Move()
    {
        distanceOfPlayer = (int)Vector3.Distance(transform.position, player.position);

        if (this.agent.isStopped == false) 
        {
            base.Move();

            // 플레이어와의 거리가 인식범위보다 크면
            if (distanceOfPlayer > agroRange)
            {
                // 졸개 타입에 따라 구른다
                transform.Find("Ani").transform.Rotate(transform.right * agent.speed * 5f, Space.World);

                #region Legacy
                //if (gameObject.name.Contains("Base")) 
                //{
                //    transform.Find("Ani").transform.Rotate(transform.right * agent.speed * 5f, Space.World);
                //}
                //else if (gameObject.name.Contains("Fast")) 
                //{
                //    transform.Find("Ani").transform.Rotate(transform.right * agent.speed * 5f, Space.World);
                //}
                #endregion
            }
            // 플레이어와의 거리가 인식범위보다 작으면
            else if (distanceOfPlayer <= agroRange)
            {
                // 멈춘다
                this.agent.speed = 0f;
                Debug.LogWarning(agent.isStopped);
            }
        }
    }

    protected override void Attack()
    {
        GameManager.instance.HpMin(damage);
    }

    protected override void GetHit(Collider other, int damage_)
    {
        healthPoint -= damage_;

        if (healthPoint <= 0) 
        {
            // 거리 내에 플레이어 존재 시 데미지 가함
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
