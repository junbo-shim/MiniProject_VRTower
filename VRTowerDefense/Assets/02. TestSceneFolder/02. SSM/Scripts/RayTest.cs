using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayTest : MonoBehaviour
{
    public LayerMask uiLayer; // UI를 나타내는 레이어

    void Update()
    {
        // 마우스 위치로 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 레이캐스트를 시도하고 UI 레이어만 고려
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiLayer))
        {
            // UI 오브젝트에 레이가 맞았을 경우
            Debug.Log("UI 오브젝트에 레이가 맞았습니다.");

            // 이벤트 시스템을 이용하여 UI 이벤트 전달
            ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
    }
}
