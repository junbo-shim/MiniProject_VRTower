using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimTarget : MonoBehaviour
{
    private Transform player;
    private Transform boss;
    private float distance;
    private float aimHeight;
    private float distanceHeightRatio;

    private void Awake()
    {
        FindBossObject();
        GetRatio();
    }

    private void FixedUpdate()
    {
        AdjustAimHeight();
    }

    private void FindBossObject() 
    {
        player = GameObject.Find("Player").transform;
        boss = GameObject.Find("EarthGolem").transform;
    }
    
    private void GetRatio() 
    {
        distance = Vector3.Distance(player.position, boss.position);
        aimHeight = transform.position.y - player.position.y;

        distanceHeightRatio = aimHeight / distance;
    }

    private void AdjustAimHeight() 
    {

        

    }
}
