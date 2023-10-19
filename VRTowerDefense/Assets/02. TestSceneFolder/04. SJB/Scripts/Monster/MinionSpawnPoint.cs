using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnPoint : MonoBehaviour
{
    // 기준점 = player location
    private Transform player;
    // 원의 반지름 = scale.x 값의 절반을 받아온다
    private float circleHalf;
    // 스폰 포인트를 담아둘 List
    public List<Vector3> spawnPoints;

    private Vector3 high;

    private void Awake()
    {
        spawnPoints = new List<Vector3>();
        high = new Vector3(1, 30, 1);
        GetCircleHalf();
        FindPlayer();
        GetVectorFromAngle();
    }

    private void FindPlayer() 
    {
        player = GameObject.Find("Player").transform;
    }

    private void GetCircleHalf() 
    {
        // 원의 반지름은 scale 값에서 절반을 구하면 된다
        circleHalf = transform.localScale.x * 0.5f;
    }

    private void GetVectorFromAngle() 
    {
        // Degree(각도) to Radian(파이값)으로 변환
        float rad = Mathf.Deg2Rad;

        for (int i = 10; i < 180; i += 10) 
        {
            // 보스가 다니는 길(90도)만 생성 안함
            if (i == 90) 
            {
                /*Do Nothing*/
            }
            else 
            {
                // spawnPoint 에 (Cos * 반지름, 기존 y값, sin * 반지름) 을 담는다
                Vector3 spawnPoint =
                    new Vector3(player.position.x + (Mathf.Cos(i * rad) * circleHalf),
                    player.position.y,
                    player.position.z + (Mathf.Sin(i * rad) * circleHalf));

                // 레이 발사를 위해서 위쪽에서 생성

                // 스폰 포인트 List 에 spawnPoint 들을 담아둔다
                spawnPoints.Add(spawnPoint);

                //GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //obj.GetComponent<BoxCollider>().enabled = false;
                //obj.transform.position = spawnPoint;
                //obj.transform.localScale = high;
            }
        }
    }
}
