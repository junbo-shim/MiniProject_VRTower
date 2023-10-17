using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public string targetTag = "Monster";  // 바라볼 대상의 태그
    public float rotationSpeed = 5.0f;   // 회전 속도
    private Animator animalAnimator;
    public ParticleSystem particleSystem;
    public ParticleSystem particleSystem2;

    private void Awake()
    {
        animalAnimator = GetComponent<Animator>();
    }

    private void Start()
    {


        particleSystem.Play();
        particleSystem2.Play();

    }


    void Update()
    {
        // 주변에 특정 태그를 가진 오브젝트를 찾습니다.
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length > 0)
        {
            // 가장 가까운 대상을 찾습니다.
            GameObject closestTarget = FindClosestTarget(targets);

            if (closestTarget != null)
            {
                // 대상을 향한 방향을 계산합니다.
                Vector3 direction = closestTarget.transform.position - transform.position;

                // 대상을 바라보기 위한 회전을 계산합니다.
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                // 부드러운 회전을 위해 회전을 보간합니다.
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

                animalAnimator.SetBool("Shoot", true);

            }
        }


    }



    GameObject FindClosestTarget(GameObject[] targets)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, currentPosition);

            if (distance < closestDistance)
            {
                closestTarget = target;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }


}
