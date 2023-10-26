using System.Collections;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private WaitForSecondsRealtime effectDuration;
    private ProjectileEffectPool effectPool;
    private AudioSource audioSource;
    public AudioClip ExAudio;

    private void Awake()
    {
        effect = gameObject.GetComponent<ParticleSystem>();
        effectDuration = new WaitForSecondsRealtime(2f);
        audioSource = gameObject.GetComponent<AudioSource>();
        effectPool = transform.parent.GetComponent<ProjectileEffectPool>();
    }

    private void OnEnable()
    {
        StartCoroutine(EffectSequence());
    }

    private IEnumerator EffectSequence() 
    {
        effect.Play();
        audioSource.PlayOneShot(ExAudio);

        yield return effectDuration;

        effect.Stop();
        effectPool.ReturnPoolObject(gameObject);
    }
}
