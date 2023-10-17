using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //{ 데이터 테이블 사용할 변수
    public int bulletAtk = 5;                   // 총알의 공격력
    public float bulletCriticalRate = default;  // 총알의 치명타 확률
    public float bulletCriticalDmg = default;   // 총알의 치명타 데미지
    private float speed = 5.0f;                 // 탄속
    private float lifeTime = 5.0f;              // 라이프타임
    //} 데이터 테이블 사용할 변수

    Quaternion rotation;

    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 direction_)
    {
        direction = direction_;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        Invoke("ReturnBullet", lifeTime);
    }

    public void ReturnBullet()
    {
        ObjectPoolManager.ReturnObject(this);
    }

}
