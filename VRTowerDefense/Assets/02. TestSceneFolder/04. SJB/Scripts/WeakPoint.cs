using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private Transform boss;
    public ParticleSystem hitEffect;
    public AudioSource weakAudioSource;

    public Material defaultMaterial;
    public Material weakTimeMaterial;
    public Material inactiveMatarial;

    private Vector3 normalSize;
    private WaitForSecondsRealtime weakTime;
    private WaitForSecondsRealtime inactiveTime;
    private WaitForSecondsRealtime feverHitTime;

    private bool isWeakOn = false;

    private void Awake()
    {
        boss = GameObject.Find("EarthGolem").transform;
        weakAudioSource = transform.GetComponent<AudioSource>();
        feverHitTime = new WaitForSecondsRealtime(1f);

        // 기본 사이즈와 컬러를 캐싱
        normalSize = transform.localScale;
        defaultMaterial = gameObject.GetComponent<MeshRenderer>().material;
        weakTime = boss.GetComponent<Boss>().weakTime;
        inactiveTime = boss.GetComponent<Boss>().inactiveTime;
    }

    public void StartWeakRoutine() 
    {
        StartCoroutine(OnOffWeakPoint());
    }

    // 약점 활성비활성화 Coroutine
    public IEnumerator OnOffWeakPoint() 
    {
        isWeakOn = true;
        transform.localScale *= 2;
        gameObject.GetComponent<MeshRenderer>().material = weakTimeMaterial;
        yield return weakTime;
        isWeakOn = false;
        transform.localScale = normalSize;
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
    }

    // 약점에 맞을 경우
    private void OnTriggerEnter(Collider other)
    {
        // 닿은 Collider 가 총알이면
        if (other.GetComponent<Bullet>())
        {
            // 데미지 함수를 실행한다
            boss.GetComponent<Boss>().
                GetHitWeakPoint(other, (int)other.GetComponent<Bullet>().bulletAtk);

            if (isWeakOn == false) 
            {
                StartCoroutine(InactiveForSecond());
            }
            else if (isWeakOn == true) 
            {
                StartCoroutine(FeverTime());
            }
        }
    }

    // 약점 히트 시 offon 기능
    private IEnumerator InactiveForSecond() 
    {
        hitEffect.Play();
        weakAudioSource.PlayOneShot(weakAudioSource.clip);
        gameObject.GetComponent<MeshRenderer>().material = inactiveMatarial;
        gameObject.GetComponent<MeshCollider>().enabled = false;

        yield return inactiveTime;

        hitEffect.Stop();
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    private IEnumerator FeverTime() 
    {
        hitEffect.Play();
        weakAudioSource.PlayOneShot(weakAudioSource.clip);
        yield return feverHitTime;
        hitEffect.Stop();
    }
}
