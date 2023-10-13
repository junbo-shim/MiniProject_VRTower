using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    // 인터페이스로 구현함으로써 playerController클래스에서 이 인터페이스로 호출합니다.
    //! 상태가 시작될 때 호출한다.
    void OnEnter(PlayerController player);
    //! 상태가 유지되는 동안 호출한다.
    void Update();
    //! 상태를 탈출할 때 호출한다.
    void OnExit();
}


