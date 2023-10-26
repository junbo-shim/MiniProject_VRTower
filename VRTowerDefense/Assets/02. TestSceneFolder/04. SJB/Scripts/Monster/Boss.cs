using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonBase
{
    #region 괴수 변수

    // 괴수 Scriptable
    private ScriptableObj_Boss bossData;
    // 투사체 Scriptable
    private ScriptableObj_Projectile projectileData;
    // 기본졸개 Scriptable
    private ScriptableObj_Minion baseMinionData;
    // 빠른졸개 Scriptable
    private ScriptableObj_Minion fastMinionData;

    // 괴수 시작 지점
    private Vector3 startPos;
    // 괴수 HP 게이지    
    private Image hpGauge;
    // 오브젝트 풀링 관련 변수
    private Transform[] fireHolders;
    private ProjectilePool projectilePool;
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;
    private SpawnEffectPool spawnEffectPool;

    private ObjectPool bulletPool;
    private ObjectPool iceBulletPool;
    // 졸개 스폰포인트 관련 변수
    private List<Vector3> spawnPoints;
    private List<int> spawnPointIdxs;
    private Vector3 spawnAdjustHeight;
    // 괴수 애니메이터 및 사운드
    public Animator bossAnimator;
    public AudioSource bossAudioSource;
    public AudioClip bossMoveClip;
    public AudioClip bossAttackClip;
    public AudioClip bossDieClip;

    private Color debuffColor;
    // 코루틴 캐싱 변수
    private WaitForSecondsRealtime minionSpawnTime;
    private WaitForSecondsRealtime fireTime;
    public WaitForSecondsRealtime weakTime;
    public WaitForSecondsRealtime inactiveTime;
    public WaitForSecondsRealtime attackWaitTime;
    // 괴수 약점 오브젝트 리스트
    public List<WeakPoint> weakPointList;
    // 괴수 약점 활성화 갯수 확인 변수
    public int activatedWeakPoint = default;
    // 괴수 슬로우 스택
    public int slowStack = default;

    #endregion

    private void Awake()
    {
        Init();
        StartCoroutine(GetUpSequence());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            ActivateWeakPoint();
        }   
    }

    private void FixedUpdate()
    {
        if (healthPoint > 0) 
        {
            Move();
        }
        else if (healthPoint <= 0) 
        {
        
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region AREA
        // 닿은 Trigger Collider 의 이름에 Area 라는 문자가 포함되어 있다면
        if (other.name.Contains("Area"))
        {
            spawnPoints.Clear();
            spawnPointIdxs.Clear();
            // 그 Area 의 spawnPoints 를 보스의 spawnPoints 에 담는다
            spawnPoints = other.GetComponent<MinionSpawnPoint>().spawnPoints;

            // 공격 시작 (애니메이션, 소리 및 오브젝트 소환)
            StartCoroutine(AttackCoroutine());

            // 그리고 닿은 오브젝트를 끈다
            other.gameObject.SetActive(false);
        }
        #endregion

        #region PC Bullet
        // 닿은 Collider 가 총알(Bullet)면
        if (other.GetComponent<Bullet>())
        {
            // 골드 획득
            GameManager.instance.AddCoin(100);
            // 데미지 함수를 실행한다
            GetHit(other, (int)other.GetComponent<Bullet>().bulletAtk);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
            other.GetComponent<Bullet>().ReturnBullet();
        }
        #endregion

        #region Turret Bullet
        // 닿은 Collider 가 총알(TurretShoot)이면
        if (other.GetComponent<ShootBullet>() && other.name.Contains("Ice"))
        {
            // 데미지 함수를 실행한다
            GetHit(other, (int)other.GetComponent<ShootBullet>().bulletData.damage);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
            // ice bullet 일 경우 해결 해야함
            if (slowStack < 5) 
            {
                StartCoroutine(
                    SlowCoroutine(other.GetComponent<ShootBullet>().bulletData.debuffDuration,
                    other.GetComponent<ShootBullet>().bulletData.slow));
            }
            iceBulletPool.ReturnObjectToPool(other.gameObject);
        }
        else if (other.GetComponent<ShootBullet>()) 
        {
            // 데미지 함수를 실행한다
            GetHit(other, (int)other.GetComponent<ShootBullet>().bulletData.damage);
            // 실행 후에 오브젝트 풀로 돌아가게 만들어야함
            bulletPool.ReturnObjectToPool(other.gameObject);
        }
        #endregion

        #region Player
        // 닿은 Collider 가 플레이어면
        if (other.GetComponent<CharacterController>()) 
        {
            this.agent.speed = 0f;
            bossAnimator.SetTrigger("Defeat");
            // 체력 전체 마이너스
            GameManager.instance.HpMin(GameManager.instance.playerHp);
        }
        #endregion
    }

    #region 애니메이션 및 사운드
    // 일어나는 애니메이션 코루틴
    private IEnumerator GetUpSequence() 
    {
        yield return new WaitForSecondsRealtime(5f);
        // 걷기 시작 및 agent 움직임 on
        bossAnimator.SetBool("IsStart", true);
        this.agent.speed = bossData.moveSpeed;
    }
    // 보스 움직임과 소리
    protected override void Move()
    {
        base.Move();
    }
    public void Walk() 
    {
        bossAudioSource.PlayOneShot(bossMoveClip);
    }
    public void Roar() 
    {
        bossAudioSource.PlayOneShot(bossAttackClip);
    }
    public void Fall() 
    {
        bossAudioSource.PlayOneShot(bossDieClip);
    }
    // 슬로우 코루틴
    private IEnumerator SlowCoroutine(int slowtime, float slowMulti)
    {
        bossAnimator.speed *= slowMulti;
        slowStack += 1;
        agent.speed *= slowMulti;

        Color debuffColor = new Color
        (
            transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color.r - 0.2f,
            transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color.g - 0.1f,
            transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color.b,
            transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color.a
        );

        transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color
            -= debuffColor;
        transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[1].color
            -= debuffColor;

        yield return new WaitForSecondsRealtime((float)slowtime);

        bossAnimator.speed /= slowMulti;
        slowStack -= 1;
        agent.speed /= slowMulti;
        transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color
            += debuffColor;
        transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[1].color
            += debuffColor;
    }
    #endregion

    // 초기화 메서드
    protected override void Init()
    {
        base.Init();
        // 오브젝트 풀링 찾기
        FindFireHolder();
        FindProjectilePool();
        FindBaseMinionPool();
        FindFastMinionPool();
        FindSpawnEffectPool();
        bulletPool = GameObject.Find("Bullets").gameObject.GetComponent<ObjectPool>();
        iceBulletPool = GameObject.Find("IceBullets").gameObject.GetComponent<ObjectPool>();

        debuffColor = transform.Find("mo").GetComponent<SkinnedMeshRenderer>().materials[0].color;
        // 약점 찾기
        FindWeakPoint();

        // Scriptable 할당
        bossData = (ScriptableObj_Boss)CSVConverter_SJB.Instance.ScriptableObjDictionary[1001];
        projectileData = (ScriptableObj_Projectile)CSVConverter_SJB.Instance.ScriptableObjDictionary[2001];
        baseMinionData = (ScriptableObj_Minion)CSVConverter_SJB.Instance.ScriptableObjDictionary[3001];
        fastMinionData = (ScriptableObj_Minion)CSVConverter_SJB.Instance.ScriptableObjDictionary[3002];

        // startPos 할당 및 괴수 초기위치 설정
        startPos = new Vector3(3f, 0.27f, 287.5f);
        gameObject.transform.position = startPos;

        // 괴수 애니메이터 및 사운드 할당
        bossAnimator = gameObject.GetComponent<Animator>();
        bossAudioSource = gameObject.GetComponent<AudioSource>();

        // HP 게이지 할당
        hpGauge = gameObject.transform.Find("Canvas").Find("Gauge").GetComponent<Image>();

        // 졸개 소환 위치 관련 변수
        spawnPoints = new List<Vector3>();
        spawnPointIdxs = new List<int>();
        spawnAdjustHeight = new Vector3(0f, 20f, 0f);

        // 모든 변수는 CSV 로 읽어와야하며 배율도 수정해야함
        // 소환 이펙트 - 졸개 소환 코루틴 사이 yield return time
        minionSpawnTime = new WaitForSecondsRealtime(1f);
        // 투사체 발사를 위해 대기하는 yield return time
        fireTime = new WaitForSecondsRealtime(projectileData.coolTime);
        // 피버타임 지속시간
        weakTime = new WaitForSecondsRealtime(8f);
        // 괴수 약점 재활성화 시간
        inactiveTime = new WaitForSecondsRealtime((float)bossData.weakPointDuration);
        // 괴수 공격 시 애니메이션과 일치시키기 위한 yield return time
        attackWaitTime = new WaitForSecondsRealtime(bossData.atkCoolTime * 0.1f);

        // 괴수 이동속도
        this.agent.speed = 0f;
        // 괴수 체력
        this.healthPoint = bossData.hp;
        // 괴수 공격 쿨타임
        this.attackCooltime = bossData.atkCoolTime;
        // 괴수 최대 체력
        this.maxHealthPoint = healthPoint;
    }


    #region 데미지 처리
    // 데미지 차감 함수 및 체력 게이지 업데이트
    protected override void GetHit(Collider other, int damage)
    {
        // 체력에서 데미지 감산
        healthPoint -= damage;
        // 체력 게이지 업데이트
        hpGauge.fillAmount = (float)healthPoint / (float)maxHealthPoint;

        // 만약 남은 체력이 0 초과면
        if (healthPoint > 0) 
        {
            /* Do Nothing */
        }
        // 만약 0 이하이면
        else if (healthPoint <= 0) 
        {
            // Die 애니메이션이 활성화되지 않은 경우만
            if (bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die") == false) 
            {
                Die();
            }
        }
    }
    // Weak Point 에서 사용할 수 있도록 열어준 GetHit 메서드
    public void GetHitWeakPoint(Collider other, int damage)
    {
        // 데미지 배율 적용
        this.GetHit(other, (int)(damage * bossData.weakPointMultiflier));
    }
    #endregion

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
        CheckWeakPointToOn();
    }
    private void CheckWeakPointToOn() 
    {
        int num = Random.Range(0, weakPointList.Count);
        bool isPointFound = false;

        while (isPointFound == false) 
        {
            if (activatedWeakPoint == weakPointList.Count) 
            {
                break;
            }

            if (weakPointList[num].isFeverOn == true)
            {
                num = Random.Range(0, weakPointList.Count);
            }
            else if (weakPointList[num].isFeverOn == false)
            {
                weakPointList[num].StartWeakRoutine();
                activatedWeakPoint += 1;
                //Debug.LogError(activatedWeakPoint);
                isPointFound = true;
            }
        }
    }

    #endregion

    #region 투사체 관련 기능
    // 보스의 투사체 발사 위치 할당
    private void FindFireHolder()
    {
        // projectileData 에서 값을 받아와 할당
        // 왜 배열 크기로 하려는 projectileData.respawnNumber 가 null 인지 모르겠음
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
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot() 
    {
        int num = 0;

        while (num < fireHolders.Length) 
        {
            GameObject obj = projectilePool.GetPoolObject();
            obj.transform.position = fireHolders[num].position;

            num++;
            yield return fireTime;
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

        for (int i = 0; i < 2; i++)
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
            // 이펙트를 먼저 생성
            GameObject effect = spawnEffectPool.GetPoolObject();
            effect.transform.position = spawnPoints[spawnPointIdxs[i]];
            // 이펙트 반납 후 졸개 생성
            StartCoroutine("ReturnEffect", effect);
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


    // 보스 공격 코루틴
    private IEnumerator AttackCoroutine()
    {
        // 애니메이터의 IsAreaTouched 값 변경
        bossAnimator.SetBool("IsAreaTouched", true);
        agent.speed = 0f;
        yield return attackWaitTime;

        SpawnMinion();
        ChargeProjectile();

        agent.speed = bossData.moveSpeed;
        bossAnimator.SetBool("IsAreaTouched", false);
    }


    // 보스 죽음
    protected override void Die()
    {
        base.Die();
        agent.speed = 0f;
        // 애니메이션 트리거 on
        bossAnimator.SetTrigger("Die");
    }
}
