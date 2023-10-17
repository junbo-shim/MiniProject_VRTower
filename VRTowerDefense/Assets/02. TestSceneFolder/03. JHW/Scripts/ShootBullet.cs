using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    private BulletData bulletData;
    private Rigidbody rb;
    private int chk = 0;
    private void Awake()
    {
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
       
        Debug.Log("a");
        Invoke("ReturnBullet", chk);
        
      
    }
    private void Update()
    {
      
      
        rb.velocity = transform.forward * bulletData.speed;

     


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
}
