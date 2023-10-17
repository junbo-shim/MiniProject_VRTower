using UnityEngine;

public class MonBase : MonoBehaviour
{
    // 몬스터의 Rigidbody
    protected Rigidbody rigid;
    // 플레이어의 위치
    public Transform player;
    // 몬스터가 이동하는 속도
    protected float moveSpeed;
    // 몬스터의 공격 쿨타임
    protected float attackCooltime;
    // 몬스터의 체력
    protected float healthPoint;
    // 몬스터의 데미지
    protected float damage;
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
        player = GameObject.Find("Player").transform;
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    protected virtual void Move() 
    {
        Vector3 playerDirection = 
            new Vector3(player.position.x, 0f, player.position.z);
        Vector3 objectDirection = 
            new Vector3(transform.position.x, 0f, transform.position.z);


        // Rigidbody 의 velocity 를 이동 속도 * 시간 * 바라보는 방향 으로 부여한다
        rigid.velocity = 
            moveSpeed * Time.deltaTime * (playerDirection - objectDirection).normalized;
    }

    protected virtual void Attack() 
    {
        
    }

    protected virtual void GetHit() 
    {
    
    }

    protected virtual void Die() 
    {
    
    }

}
