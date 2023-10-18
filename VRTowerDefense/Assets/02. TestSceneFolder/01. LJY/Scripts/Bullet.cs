using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //{ 데이터 테이블 사용할 변수
    public float bulletAtk = 5f;                   // 총알의 공격력
    public float bulletCriticalRate = default;  // 총알의 치명타 확률
    public float bulletCriticalDmg = default;   // 총알의 치명타 데미지
    private float speed = 10.0f;                 // 탄속
    private float lifeTime = 5.0f;              // 라이프타임
    //} 데이터 테이블 사용할 변수
    private Vector3 direction;

    private void Start()
    {       
        //transform.localRotation = ARAVR_Input.RHandDirection;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Shoot(Vector3 direction_)
    {
        direction = direction_;
        //rotation = Quaternion.LookRotation(direction);
        //transform.rotation = rotation;
        Invoke("ReturnBullet", lifeTime);
    }

    public void ReturnBullet()
    {
        ObjectPoolManager.ReturnObject(this);
    }

}
