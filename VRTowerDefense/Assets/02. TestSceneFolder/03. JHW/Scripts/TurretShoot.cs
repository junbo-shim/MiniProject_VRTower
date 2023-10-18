using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public string bulletPoolName = "BulletPool"; // 오브젝트 풀 이름
    public Transform firePoint;
    public float fireRate = 0.5f;
    public BulletData bulletData;

    private float nextFireTime = 0f;

 



    private void Update()
    {
        nextFireTime += Time.deltaTime;
        if(nextFireTime > fireRate)
        {
            nextFireTime = 0f;
            Shoot();
        }
            
        
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPoolHelper.Instance.GetObjectFromPool(
            bulletPoolName, firePoint.position, firePoint.rotation
        );

        if (bullet != null)
        {
            ShootBullet bulletScript = bullet.GetComponent<ShootBullet>();
            bullet.SetActive(true);
            //bulletScript.Initialize(bulletData);
        }
        else
        {
            
        }
    }
}