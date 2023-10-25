using System.Collections;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private WaitForSecondsRealtime effectDuration;
    private ProjectileEffectPool effectPool;

    private void Awake()
    {
        effect = gameObject.GetComponent<ParticleSystem>();
        effectDuration = new WaitForSecondsRealtime(2f);
        effectPool = transform.parent.GetComponent<ProjectileEffectPool>();
    }

    private void OnEnable()
    {
        StartCoroutine(EffectSequence());
    }

    private IEnumerator EffectSequence() 
    {
        effect.Play();
        yield return effectDuration;
        effect.Stop();
        effectPool.ReturnPoolObject(gameObject);
    }
}
