using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonBase
{
    private Image hpGauge;
    private Transform[] fireHolders;
    public ProjectilePool projectilePool;
    public BaseMinionPool baseMinionPool;
    public FastMinionPool fastMinionPool;
    public SpawnEffectPool spawnEffectPool;
    private List<Vector3> spawnPoints;
    private List<int> spawnPointIdxs;
    private WaitForSecondsRealtime minionSpawnTime;
    public WaitForSecondsRealtime weakTime;
    public List<WeakPoint> weakPointList;

    public float slowTime;
    public float slowAmount;
    public int slowStack = default;

    private void Awake()
    {
        Init();
        ChargeProjectile();
    }

    private void FixedUpdate()
    {
        base.Move();

        if (Input.GetKeyDown(KeyCode.A)) 
        {
            Debug.Log("??");
            ActivateWeakPoint();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 닿은 Trigger Collider 의 이름에 Area 라는 문자가 포함되어 있다면
        if (other.name.Contains("Area"))
        {
            spawnPoints.Clear();
            spawnPointIdxs.Clear();
            // 그 Area 의 spawnPoints 를 보스의 spawnPoints 에 담는다
            spawnPoints = other.GetComponent<MinionSpawnPoint>().spawnPoints;

            SpawnMinion();
            //ChargeProjectile();

            // 그리고 닿은 오브젝트를 끈다
            other.gameObject.SetActive(false);
        }

        // 닿은 Collider 가 총알(Bullet)면
        if (other.GetComponent<Bullet>() == true) 
        {
            // 데미지 함수를 실행한다
            GetHit(other, (int)other.GetComponent<Bullet>().bulletAtk);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
            other.GetComponent<Bullet>().ReturnBullet();
        }
        // 닿은 Collider 가 총알(TurretShoot)이면
        if (other.GetComponent<TurretShoot>() == true) 
        {
            // 데미지 함수를 실행한다
            GetHit(other, (int)other.GetComponent<TurretShoot>().bulletData.damage);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
            other.GetComponent<GameObject>().SetActive(false);
            // ice bullet 일 경우 해결 해야함
        }
    }
    // 초기화 메서드
    protected override void Init()
    {
        base.Init();
        FindFireHolder();
        FindProjectilePool();
        FindBaseMinionPool();
        FindFastMinionPool();
        FindSpawnEffectPool();
        FindWeakPoint();

        hpGauge = gameObject.transform.Find("Canvas").Find("Gauge").GetComponent<Image>();

        // 모든 변수는 CSV 로 읽어와야하며 배율도 수정해야함
        minionSpawnTime = new WaitForSecondsRealtime(0.5f);
        weakTime = new WaitForSecondsRealtime(0.5f);
        spawnPoints = new List<Vector3>();
        spawnPointIdxs = new List<int>();

        this.healthPoint = 500;
        this.moveSpeed = 70f;
        this.attackCooltime = 5f;
        this.maxHealthPoint = healthPoint;
    }
    // Weak Point 에서 사용할 수 있도록 열어준 GetHit 메서드
    public void GetHitWeakPoint(Collider other, int damage) 
    {
        this.GetHit(other, (int)(damage * 1.5f));
    }
    // 데미지 차감 함수 및 체력 게이지 업데이트
    protected override void GetHit(Collider other, int damage)
    {
        healthPoint -= damage;
        hpGauge.fillAmount = (float)healthPoint / (float)maxHealthPoint;
    }

    #region 약점 관련 기능
    // 약점 찾아오는 메서드
    private void FindWeakPoint() 
    {
        weakPointList = new List<WeakPoint>();
        weakPointList.AddRange(FindObjectsOfType<WeakPoint>());
    }
    // 넘겨줄 약점 활성화 메서드
    public void ActivateWeakPoint() 
    {
        int num = Random.Range(0, weakPointList.Count);
        Debug.LogWarning(num);
        weakPointList[num].MakeBossWeak();
        Debug.LogWarning(weakPointList[num]);
    }
    #endregion

    #region 투사체 관련 기능
    // 보스의 투사체 발사 위치 할당
    private void FindFireHolder()
    {
        fireHolders = new Transform[5];
        Transform fireHolder = gameObject.transform.Find("FireHolder").transform;

        for (int i = 0; i < fireHolder.childCount; i++) 
        {
            fireHolders[i] = fireHolder.GetChild(i);
        }
    }
    // 투사체 Object Pool 가져오기
    private void FindProjectilePool() 
    {
        projectilePool = 
            GameObject.Find("PoolControl").
            transform.Find("Projectile Pool").
            gameObject.GetComponent<ProjectilePool>();
    }
    // 투사체 Object 대여
    private void ChargeProjectile()
    {
        for (int i = 0; i < fireHolders.Length; i++) 
        {
            GameObject obj = projectilePool.GetPoolObject();
            obj.transform.position = fireHolders[i].position;
            obj.transform.SetParent(fireHolders[i], true);
        }
    }
    #endregion

    #region 졸개 관련 기능
    // 기본 졸개 Object Pool 가져오기
    private void FindBaseMinionPool() 
    {
        baseMinionPool =
            GameObject.Find("PoolControl").
            transform.Find("BaseMinion Pool").
            gameObject.GetComponent<BaseMinionPool>();
    }
    // 빠른 졸개 Object Pool 가져오기
    private void FindFastMinionPool() 
    {
        fastMinionPool =
            GameObject.Find("PoolControl").
            transform.Find("FastMinion Pool").
            gameObject.GetComponent<FastMinionPool>();
    }
    // 졸개 소환 프리팹 Object Pool 가져오기
    private void FindSpawnEffectPool() 
    {
        spawnEffectPool =
            GameObject.Find("PoolControl").
            transform.Find("SpawnEffect Pool").
            gameObject.GetComponent<SpawnEffectPool>();
    }
    // 랜덤한 스폰포인트 인덱스 뽑기
    private void GetRandomPoint(int min, int max) 
    {
        int randomNum = Random.Range(min, max);

        for (int i = 0; i < 5; i++)
        {
            if (randomNum >= max * 0.5f)
            {
                spawnPointIdxs.Add(randomNum);
                randomNum -= 1;
            }
            else if (randomNum < max * 0.5f)
            {
                spawnPointIdxs.Add(randomNum);
                randomNum += 1;
            }
        }
    }
    // 몬스터 스폰하는 메서드
    private void SpawnMinion() 
    {
        GetRandomPoint(0, spawnPoints.Count);

        for (int i = 0; i < spawnPointIdxs.Count; i++) 
        {
            // 생성 위치에서 레이 아래로 발사, spawnPoint 재설정
            CheckSpawnHeight(spawnPointIdxs[i]);
            // 이펙트를 먼저 생성
            GameObject effect = spawnEffectPool.GetPoolObject();
            effect.transform.position = spawnPoints[spawnPointIdxs[i]];
            // 이펙트 반납 후 졸개 생성
            StartCoroutine("ReturnEffect", effect);
            // 생성 대기
            //StartCoroutine("WaitForOneSecond");
        }
    }
    // 스폰 포인트에서 레이를 쏴서 높이에 맞게 spawnPoints 배열을 다시 설정하는 메서드
    private void CheckSpawnHeight(int spawnPointIdx) 
    {
        RaycastHit hit = new RaycastHit();

        //Physics.Raycast(spawnPoints[spawnPointIdx], Vector3.down, out hit);

        if (Physics.Raycast(spawnPoints[spawnPointIdx], Vector3.down, out hit)) 
        {
            Debug.LogWarning(hit);
            Debug.LogWarning(hit.transform.position);
            spawnPoints[spawnPointIdx] = hit.transform.position + Vector3.one;
            Debug.LogWarning(spawnPoints[spawnPointIdx]);
        }
    }

    // 1초 뒤 이펙트 반납하는 Coroutine
    private IEnumerator ReturnEffect(GameObject obj) 
    {
        yield return minionSpawnTime;
        // 이펙트 반납
        spawnEffectPool.ReturnPoolObject(obj);
        // 졸개 생성
        ChooseRandomMinion(obj.transform.position);
        yield return minionSpawnTime;
        yield break;
    }
    // 몬스터 순차 생성을 위해 1초간 대기하는 Coroutine
    private IEnumerator WaitForOneSecond() 
    {
        yield return minionSpawnTime;
        yield break;
    }
    // 랜덤하게 base, fast minion object 대여하기
    private void ChooseRandomMinion(Vector3 spawnPoint) 
    {
        int randomNum = Random.Range(0, 3);

        if (randomNum == 0 || randomNum == 1)
        {
            GameObject minion = baseMinionPool.GetPoolObject();
            minion.transform.position = spawnPoint;
        }
        else if (randomNum == 2)
        {
            GameObject minion = fastMinionPool.GetPoolObject();
            minion.transform.position = spawnPoint;
        }
    }
    #endregion
}
