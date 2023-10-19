using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform boss;
    public Transform target;
    private Rigidbody projectileRigid;
    private ProjectilePool projectilePool;
    private float moveSpeed = 50f;
    public int damage = 10;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        FindTarget();
        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        projectilePool.ReturnPoolObject(gameObject);
    }

    private void Init() 
    {
        projectileRigid = gameObject.GetComponent<Rigidbody>();
    }

    private void FindTarget() 
    {
        boss = GameObject.Find("EarthGolem").transform;
        target = GameObject.Find("Player").transform.Find("AimTarget");

        projectilePool = boss.GetComponent<Boss>().projectilePool;

        transform.LookAt(target, Vector3.up);
    }

    private void Move()
    {
        projectileRigid.velocity = transform.forward * moveSpeed;
    }
}
