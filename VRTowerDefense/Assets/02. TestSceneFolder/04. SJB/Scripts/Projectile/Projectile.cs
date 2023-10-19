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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player")) 
        {
            projectilePool.ReturnPoolObject(gameObject);
        }
    }

    private void Init() 
    {
        projectileRigid = gameObject.GetComponent<Rigidbody>();
    }

    private void FindTarget() 
    {
        boss = GameObject.Find("EarthGolem").transform;
        target = GameObject.Find("Player").transform.Find("OVRCameraRig");

        projectilePool = boss.GetComponent<Boss>().projectilePool;
    }

    private void Move()
    {
        transform.LookAt(target, Vector3.up);
        projectileRigid.velocity = transform.forward * moveSpeed;
    }
}
