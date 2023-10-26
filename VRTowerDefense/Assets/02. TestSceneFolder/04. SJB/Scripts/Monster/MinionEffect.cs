using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MinionEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private WaitForSecondsRealtime effectDuration;
    private MinionEffectPool effectPool;
    private AudioSource audioSource;
    public AudioClip ExAudio;

    private void Awake()
    {
        effect = gameObject.GetComponent<ParticleSystem>();
        effectDuration = new WaitForSecondsRealtime(1f);
        audioSource = gameObject.GetComponent<AudioSource>();
        effectPool = transform.parent.GetComponent<MinionEffectPool>();
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
