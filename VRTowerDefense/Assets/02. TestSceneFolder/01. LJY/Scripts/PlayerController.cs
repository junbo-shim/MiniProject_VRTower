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

    public Transform Crosshair; // 가리키는 곳에 나타날 크로스헤어Obj


    // Start is called before the first frame update
    void Start()
    {
        SetState(battleState);
    }

    // Update is called once per frame
    void Update()
    {
        // 가리키는 곳에 크로스헤어가 보이게한다.
        ARAVR_Input.DrawCrosshair(Crosshair);

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
    }

    void IState.Update()
    {

    }

    void IState.OnExit()
    {

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
