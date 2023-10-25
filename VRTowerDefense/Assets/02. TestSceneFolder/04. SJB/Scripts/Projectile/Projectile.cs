using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    private Rigidbody projectileRigid;
    public ProjectilePool projectilePool;
    public ProjectileEffectPool projectileEffectPool;

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

    private void Start()
    {
        projectileEffectPool = transform.parent.parent.Find("ProjectileEffect Pool").GetComponent<ProjectileEffectPool>();
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
            //GameObject effect = projectileEffectPool.GetPoolObject();
            //effect.transform.position = transform.position;
            projectilePool.ReturnPoolObject(gameObject);
        }

        if (other.GetComponent<Bullet>()) 
        {
            GameObject effect = projectileEffectPool.GetPoolObject();
            effect.transform.position = transform.position;
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
        transform.Find("Ani").Rotate(transform.forward * 5f, Space.World);
        transform.LookAt(target, Vector3.up);
        projectileRigid.velocity = transform.forward * moveSpeed;
    }
}
