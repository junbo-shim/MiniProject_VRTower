using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    public BulletData bulletData;
    private Rigidbody rb;
    private int chk = 0;

    public TowerDataScriptableObject projectileData;

    private void Awake()
    {
        // CSVReader로부터 읽어온 스크립터블 오브젝트 데이터 
        foreach (var data in projectileData.items)
        {
            Debug.Log("CSVID: " + data.CSVID + ", Description: " + data.Description + ", Model: " + data.Model + "Atk: " + data.Atk);
            bulletData.damage = data.Atk;
            bulletData.speed = data.projectile_Speed;
            bulletData.slow = data.Movement_Speed;
            bulletData.debuffDuration = data.Debuff;
        }

        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(BulletData data)
    {
        bulletData = data;
        rb.velocity = transform.up * bulletData.speed;
    }

    private void Start()
    {

    }

    private void OnDisable()
    {

      
        rb.velocity = Vector3.zero;

    }

    private void OnEnable()
    {
       
        Invoke("ReturnBullet", chk);
        
      
    }
    private void Update()
    {
      
      
        rb.velocity = transform.up * bulletData.speed;

     


    }

    private void ReturnBullet()
    {
        if(chk != 5)
        {
            chk = 5;
        }
      // gameObject.SetActive(false);
       ObjectPoolHelper.Instance.ReturnObjectToPool("Bullets", gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Monster"))
        {
            gameObject.SetActive(false);
        }
    }
}
