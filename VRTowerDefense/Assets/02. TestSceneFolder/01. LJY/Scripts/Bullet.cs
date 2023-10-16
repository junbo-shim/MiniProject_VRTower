using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2.0f;  // 탄속
    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * speed;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 direction_)
    {
        direction = direction_;
        Invoke("ReturnBullet", 5f);
    }

    public void ReturnBullet()
    {
        ObjectPoolManager.ReturnObject(this);
    }

}
