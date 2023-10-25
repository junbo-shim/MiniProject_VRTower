using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerProjectileDataScriptableObject data;

    //{ 데이터 테이블 사용할 변수
    public float bulletAtk = 5f;                   // 총알의 공격력
    public float bulletCriticalRate = default;  // 총알의 치명타 확률
    public float bulletCriticalDmg = default;   // 총알의 치명타 데미지
    private float speed = 30.0f;                 // 탄속
    private float lifeTime = 16f;              // 라이프타임
    //} 데이터 테이블 사용할 변수
    public ObjectPoolManager objectPoolManager;
    private Vector3 direction;

    private void Awake()
    {
        bulletAtk = data.items[0].Atk;
        bulletCriticalRate = data.items[0].Critical_Rate;
    }

    private void Start()
    {
        //transform.localRotation = ARAVR_Input.RHandDirection;
    }


    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>()) 
        {
            ReturnBullet();
        }

        if (other.GetComponent<Minion>()) 
        {
            ReturnBullet();
        }

        if (other.GetComponent<WeakPoint>()) 
        {
            ReturnBullet();
        }
    }


    public void Shoot(Vector3 direction_)
    {
        direction = direction_;
        transform.forward = direction;
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //rotation = Quaternion.LookRotation(direction);
        //transform.rotation = rotation;
        Invoke("ReturnBullet", lifeTime);
    }

    public void ReturnBullet()
    {
        objectPoolManager.ReturnObject(this);
    }

    private IEnumerator OnEffectBullet()
    {
        yield return new WaitForSeconds(2.0f);
        objectPoolManager.ReturnObject(this);
    }


}
