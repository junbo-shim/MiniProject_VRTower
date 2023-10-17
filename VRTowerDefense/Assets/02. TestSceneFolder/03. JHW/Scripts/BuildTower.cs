using UnityEngine;
using System.Collections;

public class BuildTower : MonoBehaviour
{

    public GameObject prefabToPlace;  // 설치할 프리팹
    public string targetTag = "Ground";  // 타겟 오브젝트의 태그


    public GameObject previewObject;  // 불투명한 프리팹을 표시하기 위한 오브젝트

    private FillAmount fillAmountScript; // FillAmount 스크립트에 대한 참조

    void Start()
    {
        previewObject = Instantiate(previewObject, Vector3.zero, Quaternion.identity);
        previewObject.SetActive(false);
      
    }

    void Update()
    {
        Build();
    }


    private void Build()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭했을 때
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(targetTag))
            {
                // 타겟 오브젝트와 부딪혔을 때 해당 위치에 프리팹을 설치
                GameObject tower = Instantiate(prefabToPlace, hit.point, Quaternion.identity);
                fillAmountScript = tower.GetComponent<FillAmount>();
                // FillAmount 스크립트의 코루틴을 시작합니다.
                StartCoroutine(fillAmountScript.DecreaseFillAmountOverTime());


            }
        }
        else
        {
            // 마우스 위치를 트래킹하여 프리뷰 오브젝트를 따라다니게 함
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(targetTag))
            {
                previewObject.SetActive(true);
                previewObject.transform.position = hit.point;
            }
            else
            {
                previewObject.SetActive(false);
            }
        }
    }
}
