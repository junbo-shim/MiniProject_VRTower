using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minion : MonBase
{
    private MeshRenderer mesh;
    private BaseMinionPool baseMinionPool;
    private FastMinionPool fastMinionPool;

    private AudioSource minionAudioSource;

    public ParticleSystem particleExplosion;
    public Material defaultMaterial;
    public Material transparent;
    private Color defaultColor;

    private int distanceOfPlayer;
    private int attackRange = 5;
    private int explosionRange = 10;
    private bool isAttack = false;


    private void OnEnable()
    {
        Init();
        InitSecond();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 추후에 name 에서 tag 또는 layer 로 변경
        if (other.GetComponent<Bullet>()) 
        {
            ChangeColor();
            this.GetHit(other, (int)other.GetComponent<Bullet>().bulletAtk);
        }
    }

    protected override void Init()
    {
        base.Init();

        minionAudioSource = transform.GetComponent<AudioSource>();
        particleExplosion = transform.Find("SmallExplosion").GetComponent<ParticleSystem>();

        baseMinionPool = transform.parent.GetComponent<BaseMinionPool>();
        fastMinionPool = transform.parent.GetComponent<FastMinionPool>();

        if (gameObject.name == "BaseMinion" + "(Clone)") 
        {
            // 수치 조정 필요
            this.healthPoint = 1 * 5;
            this.damage = 20;
        }
        else 
        {
            // 수치 조정 필요
            this.healthPoint = 2 * 5;
            this.damage = 20;
        }
    }

    private void InitSecond() 
    {
        mesh = transform.Find("Ani").GetComponent<MeshRenderer>();
        defaultMaterial = mesh.material;
        defaultColor = mesh.material.color;
    }

    protected override void Move()
    {
        distanceOfPlayer = (int)Vector3.Distance(transform.position, player.position);

        if (this.agent.isStopped == false) 
        {
            base.Move();

            if (distanceOfPlayer > attackRange)
            {
                if (gameObject.name.Contains("Base")) 
                {
                    transform.Find("Ani").transform.Rotate(transform.right * 5f, Space.World);
                }
                else if (gameObject.name.Contains("Fast")) 
                {
                    transform.Find("Ani").transform.Rotate(transform.right * 10f, Space.World);
                }
            }
            else if (distanceOfPlayer <= attackRange)
            {
                this.agent.isStopped = true;
            }
        }
        else if (this.agent.isStopped == true)
        {
            if (isAttack == true) 
            {
                /* Do Nothing */
            }
            else if (isAttack == false)
            {
                isAttack = true;
                StartCoroutine(Explosion(gameObject));
            }
        }
    }

    protected override void Attack()
    {
        Debug.LogWarning("폭탄 딜 : " + damage);
        GameManager.instance.HpMin(damage);
    }

    protected override void GetHit(Collider other, int damage_)
    {
        healthPoint -= damage_;

        if (healthPoint <= 0) 
        {
            StartCoroutine(Explosion(gameObject));

            if (distanceOfPlayer <= explosionRange)
            {
                GameManager.instance.HpMin(damage);
            }
            else if (distanceOfPlayer > explosionRange)
            {
                /* Do Nothing */
            }
        }
    }


    private void ChangeColor() 
    {
        ChangeRed();
        Invoke("ChangeDefault", 0.5f);
    }
    private void ChangeRed() 
    {
        mesh.material.color = Color.red;
    }
    private void ChangeDefault() 
    {
        mesh.material.color = defaultColor;
    }


    private void CheckReturnPool(GameObject obj) 
    {

        if (obj.name == "BaseMinion" + "(Clone)") 
        {
            baseMinionPool.ReturnPoolObject(obj);
        }
        else 
        {
            fastMinionPool.ReturnPoolObject(obj);
        }
    }

    private IEnumerator Explosion(GameObject obj) 
    {
        mesh.material = transparent;
        particleExplosion.Play();
        minionAudioSource.PlayOneShot(minionAudioSource.clip);
        Attack();
        yield return new WaitForSecondsRealtime(1.2f);
        particleExplosion.Stop();
        mesh.material = defaultMaterial;
        isAttack = false;
        CheckReturnPool(obj);
    }
}
