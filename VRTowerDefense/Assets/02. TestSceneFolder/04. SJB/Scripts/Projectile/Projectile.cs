using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Scriptable 
    private ScriptableObj_Projectile projectileData;
    // 공격 대상
    private Transform target;
    // 리지드바디
    private Rigidbody projectileRigid;
    // 오브젝트 풀
    private ProjectilePool projectilePool;
    private ProjectileEffectPool projectileEffectPool;
    // 투사체 속성
    private float moveSpeed;
    private int damage;


    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        FindTarget();
    }

    private void Start()
    {
        // 다른 스크립트에서 Awake 로 초기화하므로 Awake 보다 늦게 Pool 찾기
        projectilePool = transform.parent.GetComponent<ProjectilePool>();
        projectileEffectPool = transform.parent.parent.Find("ProjectileEffect Pool").GetComponent<ProjectileEffectPool>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 부딪힐 때
        if (other.name.Contains("Player")) 
        {
            GameManager.instance.HpMin(damage);
            projectilePool.ReturnPoolObject(gameObject);
        }
        // 플레이어의 Bullet 과 부딪힐 때
        if (other.GetComponent<Bullet>()) 
        {
            GameObject effect = projectileEffectPool.GetPoolObject();
            effect.transform.position = transform.position;
            projectilePool.ReturnPoolObject(gameObject);
        }
    }

    // 초기화
    private void Init() 
    {
        // Scriptable 할당
        projectileData = (ScriptableObj_Projectile)CSVConverter_SJB.Instance.ScriptableObjDictionary[2001];
        // rigidbody 할당
        projectileRigid = gameObject.GetComponent<Rigidbody>();
        // damage 할당
        damage = projectileData.damage;
    }

    private void FindTarget() 
    {
        target = GameObject.Find("Player").transform.Find("OVRCameraRig");

        // OnEnable 시마다 새롭게 속도 할당
        moveSpeed = Random.Range(projectileData.minSpeed, projectileData.maxSpeed);
    }

    private void Move()
    {
        // 하위 transform 만 돌리기
        transform.Find("Ani").Rotate(transform.forward * 5f, Space.World);
        transform.LookAt(target, Vector3.up);
        projectileRigid.velocity = transform.forward * moveSpeed;
    }
}
