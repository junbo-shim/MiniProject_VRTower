using UnityEngine;
using UnityEngine.AI;

public class MonBase : MonoBehaviour
{
    //SSM 23 10 23 
    public Canvas dieUi;
    //SSM 20 10 23
    protected NavMeshAgent agent;
    // 몬스터의 Rigidbody
    protected Rigidbody rigid;
    // 몬스터의 속도
    protected Vector3 rigidVelocity;
    // 몬스터의 중력
    protected float rigidGravity;
    // 플레이어의 위치
    public Transform player;
    // 몬스터가 이동하는 속도
    protected float moveSpeed;
    // 몬스터의 공격 쿨타임
    protected float attackCooltime;
    // 몬스터의 체력
    protected int healthPoint;
    // 몬스터의 최대체력
    protected int maxHealthPoint;
    // 몬스터의 데미지
    protected int damage;
    // 몬스터의 디버프 중첩 체크할 int
    protected int debuffCount;

    // 몬스터가 이동 중인지 체크하는 bool
    protected bool isMoving;
    // 몬스터가 공격 중인지 체크하는 bool
    protected bool isAttacking;
    // 몬스터가 죽었는지 체크하는 bool
    protected bool isDead;

    protected virtual void Init() 
    {
        player = GameObject.Find("Player").transform.Find("OVRCameraRig");
        agent = gameObject.GetComponent<NavMeshAgent>();
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    protected virtual void Move() 
    {
        agent.SetDestination(player.position);
    }

    protected virtual void Attack() 
    {
        
    }

    protected virtual void GetHit(Collider collider, int damage) 
    {
    
    }

    protected virtual void Die() 
    {
      //SSM.23.10.23
      // dieUi.gameObject.SetActive(true);
      //SSM.23.1023
    }

}
