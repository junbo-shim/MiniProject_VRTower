using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject basicBulletPrefab;    // 기본 총알 프리팹
    public GameObject powerBulletPrefab;    // 강화 총알 프리팹
    private float attackSpeed = 1.0f;        // 공격 속도
    private float attackReinforceSpeed = 0.3f; // 강화 공격 속도
    private float attackAfter = default;    // 공격 후 흐른 시간

    public bool isAttackable =  false;      // 공격 가능한 상태인지
    private bool isAttacking = false;       // 공격중인지 확인
    [SerializeField]
    private bool isReinforced = false;      // 강화됐는지 확인
    private float reinforceTime = 15.0f;    // 강화 지속시간

    private PlayerController playerController;
    public ObjectPoolManager[] objectPoolManagers;

    public AudioClip[] gunSounds;
    private AudioSource gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        gunAudio = gameObject.GetComponent<AudioSource>();
        gunAudio.clip = gunSounds[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.currentState != playerController.battleState)
        {
            return;
        }
        attackAfter += Time.deltaTime;
        
        if(ARAVR_Input.Get(ARAVR_Input.Button.IndexTrigger) && attackAfter > attackSpeed && !isReinforced)
        {
            ARAVR_Input.PlayVibration(ARAVR_Input.Controller.RTouch);
            ShootBasicBullet();
        }
        else if(ARAVR_Input.Get(ARAVR_Input.Button.IndexTrigger) && attackAfter > attackReinforceSpeed && isReinforced)
        {
            ARAVR_Input.PlayVibration(ARAVR_Input.Controller.RTouch);
            ShootReinforceBullet();
        }

        if(ARAVR_Input.GetUp(ARAVR_Input.Button.IndexTrigger))
        {
            isAttacking = false;
        }
    }

    public void ShootBasicBullet()
    {
        gunAudio.Play();
        attackAfter = 0f;
        var bullet = objectPoolManagers[0].GetObject();
        bullet.objectPoolManager = objectPoolManagers[0];
        bullet.transform.position = ARAVR_Input.RHand.position;
        bullet.transform.rotation = ARAVR_Input.RHand.rotation;
        Vector3 direction = ARAVR_Input.RHandDirection;
        bullet.Shoot(direction);
        //StartCoroutine(ShootBullet());
    }

    public void ShootReinforceBullet()
    {
        gunAudio.Play();
        attackAfter = 0f;
        var bullet = objectPoolManagers[1].GetObject();
        bullet.objectPoolManager = objectPoolManagers[1];
        bullet.transform.position = ARAVR_Input.RHand.position;
        bullet.transform.rotation = ARAVR_Input.RHand.rotation;
        Vector3 direction = ARAVR_Input.RHandDirection;
        bullet.Shoot(direction);
    }

    private IEnumerator ReinforceTimer()
    {
        yield return new WaitForSeconds(reinforceTime);
        EndReinforce();
    }

    public void ReinforceGun(float time)
    {
        isReinforced = true;
        reinforceTime = time;
        gunAudio.clip = gunSounds[1];
        StartCoroutine(ReinforceTimer());
    }

    public void EndReinforce()
    {
        gunAudio.clip = gunSounds[0];
        isReinforced = false;
    }
    #region Legacy Coroutine
    //private IEnumerator ShootBullet()
    //{
    //    isAttacking = true;

    //    while(isAttacking)
    //    {
    //        var bullet = ObjectPoolManager.GetObject();
    //        Vector3 direction = ARAVR_Input.RHandDirection;
    //        bullet.transform.position = ARAVR_Input.RHandPosition;
    //        bullet.Shoot(direction);

    //        yield return new WaitForSeconds(1.0f / attackSpeed);
    //    }
    //}
    #endregion
}
