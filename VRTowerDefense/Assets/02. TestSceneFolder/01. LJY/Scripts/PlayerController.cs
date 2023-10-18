using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,       // 대기 상태
        Battle,     // 전투중
        Planting,   // 유닛 설치
        Death,       // 사망
    }


    public IState currentState;
    public Idle idleState = new Idle();
    public Battle battleState = new Battle();
    public Planting plantingState = new Planting();
    public Death deathState = new Death();

    // 레이저 포인트를 발사할 라인 렌더러
    public LineRenderer lineRenderer;

    // 레이저 포인터의 최대 거리
    public float lrMaxDistance = 200f;

    // 레이저 포인터의 칼라
    public Color lazerColor;

    // 총구 쪽으로 살짝 이동하기 위한 Offset
    public float shotPointOffset = 0.15f;

    public Transform Crosshair; // 가리키는 곳에 나타날 크로스헤어Obj


    // Start is called before the first frame update

    private void Awake()
    {
        // 라인 렌더러 셋팅
        lineRenderer = GetComponent<LineRenderer>();
        lazerColor.a = 0.5f;
        lineRenderer.startColor = lazerColor;
        lineRenderer.endColor = lazerColor;
    }

    void Start()
    {
        SetState(battleState);
    }

    // Update is called once per frame
    void Update()
    {
        // 가리키는 곳에 크로스헤어가 보이게한다.
        if(currentState != plantingState)
        {
            ARAVR_Input.DrawCrosshair(Crosshair);
        }
        currentState.Update();         
    }

    //! 바꿔줄 상태를 받아와 상태를 변경한다
    public void SetState(IState nextState)
    {
        if(currentState != null)
        {
            // 현재 상태의 OnExit함수를 호출한다.
            currentState.OnExit();
        }   // if: 현재 상태가 존재할경우

        // 현재 상태를 바꿀 상태로 변경한다.
        currentState = nextState;
        // 바꾼 상태의 OnEnter함수를 호출한다.
        nextState.OnEnter(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(currentState == idleState || currentState == deathState)
        { return; }
    }
}

public class Idle : IState
{
    private PlayerController player;
    void IState.OnEnter(PlayerController player_)
    {
        player = player_;
    }

    void IState.Update()
    {
        if (ARAVR_Input.GetDown(ARAVR_Input.Button.IndexTrigger, ARAVR_Input.Controller.RTouch))
        {
            // Ray가 카메라의 위치로부터 나가도록 만든다.
            Ray ray = new Ray(ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection);
            // Ray의 충돌 정보를 저장하기 위한 변수 지정
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 200))
            {
                // TODO: 레이가 게임 시작버튼에 충돌했을 경우 State이동
                player.SetState(new Battle());
            }
        }
    }

    void IState.OnExit()
    {

    }
}

public class Battle : IState
{
    private PlayerController player;

    void IState.OnEnter(PlayerController player_)
    {
        player = player_;
    }

    void IState.Update()
    {

    }

    void IState.OnExit()
    {

    }

}

public class Planting : IState
{
    private PlayerController player;

    void IState.OnEnter(PlayerController player_)
    {
        player = player_;
        player.lineRenderer.enabled = true;
    }

    void IState.Update()
    {
        int layerMask = ((1 << LayerMask.NameToLayer("PlayerBullet")) | (1 << LayerMask.NameToLayer("UI")) | 1 << LayerMask.NameToLayer("DetectArea"));
        layerMask = ~layerMask;

        // 오른쪽 컨트롤러 기준으로 Ray를 만든다. (살짝 총구 쪽에서부터 시작하도록)
        Ray ray = new Ray(ARAVR_Input.RHandPosition + ARAVR_Input.RHand.forward * player.shotPointOffset, ARAVR_Input.RHandDirection);
        RaycastHit hitInfo;

        // 충돌이 있다면?
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            // Ray가 부딪힌 지점에 라인 그리기
            player.lineRenderer.SetPosition(0, ray.origin);
            player.lineRenderer.SetPosition(1, hitInfo.point);

            // 부딪힌 지점에 크로스 헤어 그리기
            //player.crosshairCan.transform.position = hitInfo.point;
        }

        // 충돌이 없다면?
        else
        {
            player.lineRenderer.SetPosition(0, ray.origin);
            player.lineRenderer.SetPosition(1, ray.origin + ARAVR_Input.RHandDirection * player.lrMaxDistance);

            //crosshairCan.transform.position = ray.origin + ARAVR_Input.RHandDirection * lrMaxDistance;
        }
    }       // else : 오른쪽 핸드 기준으로 레이저 포인터 만들기

    void IState.OnExit()
    {
        player.lineRenderer.enabled = false;
    }
}



public class Death : IState
{
    private PlayerController player;

    void IState.OnEnter(PlayerController player_)
    {
        player = player_;
    }

    void IState.Update()
    {

    }

    void IState.OnExit()
    {

    }
}
