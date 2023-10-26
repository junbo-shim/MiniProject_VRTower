using System.Collections;
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

    public bool isFeverOn = false;

    private void Start()
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

    // 상점 기능용 public 메서드
    public void StartWeakRoutine() 
    {
        StartCoroutine(OnOffWeakPoint());
    }

    // 약점 활성비활성화 Coroutine
    public IEnumerator OnOffWeakPoint() 
    {
        // bool 값을 true 로 하여 Boss 스크립트에서 구문 제어, 스케일 2배, 머티리얼 변화
        isFeverOn = true;
        transform.localScale *= 2;
        gameObject.GetComponent<MeshRenderer>().material = weakTimeMaterial;

        // 8초 이후
        yield return weakTime;

        // bool = false, 스케일 복구, 머티리얼 복구
        isFeverOn = false;
        transform.localScale = normalSize;
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;

        // 보스의 활성화 약점 변수 감소
        boss.GetComponent<Boss>().activatedWeakPoint -= 1;
        Debug.LogError(boss.GetComponent<Boss>().activatedWeakPoint);
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

            if (isFeverOn == false) 
            {
                StartCoroutine(InactiveForSecond());
            }
            else if (isFeverOn == true) 
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
