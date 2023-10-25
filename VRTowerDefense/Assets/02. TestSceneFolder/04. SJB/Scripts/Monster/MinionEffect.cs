using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MinionEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private WaitForSecondsRealtime effectDuration;
    private MinionEffectPool effectPool;
    private AudioSource audioSource;

    private void Awake()
    {
        effect = gameObject.GetComponent<ParticleSystem>();
        effectDuration = new WaitForSecondsRealtime(1f);
        effectPool = transform.parent.GetComponent<MinionEffectPool>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(EffectSequence());
    }

    private IEnumerator EffectSequence()
    {
        effect.Play();
        audioSource.PlayOneShot(audioSource.clip);
        yield return effectDuration;
        effect.Stop();
        effectPool.ReturnPoolObject(gameObject);
    }
}
