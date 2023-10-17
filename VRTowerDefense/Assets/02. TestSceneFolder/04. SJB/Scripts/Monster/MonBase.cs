using UnityEngine;

public class MonBase : MonoBehaviour
{
    // ������ Rigidbody
    protected Rigidbody rigid;
    // �÷��̾��� ��ġ
    public Transform player;
    // ���Ͱ� �̵��ϴ� �ӵ�
    protected float moveSpeed;
    // ������ ���� ��Ÿ��
    protected float attackCooltime;
    // ������ ü��
    protected float healthPoint;
    // ������ ������
    protected float damage;
    // ������ ����� ��ø üũ�� int
    protected int debuffCount;

    // ���Ͱ� �̵� ������ üũ�ϴ� bool
    protected bool isMoving;
    // ���Ͱ� ���� ������ üũ�ϴ� bool
    protected bool isAttacking;
    // ���Ͱ� �׾����� üũ�ϴ� bool
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


        // Rigidbody �� velocity �� �̵� �ӵ� * �ð� * �ٶ󺸴� ���� ���� �ο��Ѵ�
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
