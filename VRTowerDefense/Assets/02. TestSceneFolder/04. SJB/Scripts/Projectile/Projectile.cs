using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    private Rigidbody projectileRigid;
    public ProjectilePool projectilePool;
    private float moveSpeed = 12f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Player")) 
        {
            GameManager.instance.HpMin(damage);
            projectilePool.ReturnPoolObject(gameObject);
        }

        if (other.GetComponent<Bullet>()) 
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
        target = GameObject.Find("Player").transform.Find("OVRCameraRig");

        projectilePool = transform.parent.GetComponent<ProjectilePool>();
    }

    private void Move()
    {
        transform.LookAt(target, Vector3.up);
        projectileRigid.velocity = transform.forward * moveSpeed;
    }
}
