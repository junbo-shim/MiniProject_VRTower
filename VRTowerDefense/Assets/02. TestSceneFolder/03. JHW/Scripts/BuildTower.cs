using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BuildTower : MonoBehaviour
{

    public GameObject prefabToPlace;  // 설치할 프리팹
    public string targetTag = "Ground";  // 타겟 오브젝트의 태그
    ItemCellScaleUp[] itemCellScaleUps;
    ItemCellScaleUp itemCellScaleUp;
    public LayerMask uiLayer;
    public GameObject previewObject;  // 불투명한 프리팹을 표시하기 위한 오브젝트
    public GameObject previewObject2;  // 불투명한 프리팹을 표시하기 위한 오브젝트
    private bool buildTurret = false;

    public Canvas shopUi;
    private FillAmount fillAmountScript; // FillAmount 스크립트에 대한 참조
    [SerializeField] private string targetName = "";

    void Start()
    {
        previewObject = Instantiate(previewObject, Vector3.zero, Quaternion.identity);
        previewObject.SetActive(false);

        previewObject2 = Instantiate(previewObject2, Vector3.zero, Quaternion.identity);
        previewObject2.SetActive(false);


    }

    void Update()
    {
        ShopUi();
        if (!targetName.Equals(""))
        {
            Build();
            buildTurret = false;
        }
    }

    private void ShopUi()
    {
#if BUILD_PLATFORM_PC
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
# elif TARGET_DEVICE_OCULUS
        Ray ray = new(ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection);
        RaycastHit hit;
# endif
        if (Physics.Raycast(ray, out hit, 100f, uiLayer))
        {
            itemCellScaleUps = FindObjectsOfType<ItemCellScaleUp>();
            Debug.Log(itemCellScaleUps.Length);
            //ssm
            if (hit.collider.tag.Equals("Item"))
            {
                itemCellScaleUp = hit.collider.gameObject.GetComponent<ItemCellScaleUp>();


                itemCellScaleUp.onSizeUp();
                // 23.10.18 LJY
#if BUILD_PLATFORM_PC
                if (Input.GetMouseButtonUp(0)) // 마우스 왼쪽 버튼을 클릭했을 때
                {
#elif TARGET_DEVICE_OCULUS
                if (ARAVR_Input.GetUp(ARAVR_Input.Button.One, ARAVR_Input.Controller.RTouch))    // 오른쪽 컨트롤러 Button One을 눌렀을 때
                {
#endif
                    // 23.10.18 LJY
                    // 23.10.18 SSM 
                    int buyCoin = int.Parse(hit.collider.gameObject.transform.Find("Item_Information").Find("BntBuy").Find("BuyText").GetComponent<TMP_Text>().text.ToString());



                    if (buyCoin <= GameManager.instance.coin)
                    {
                        GameManager.instance.MinCoin(buyCoin);
                        targetName = hit.collider.gameObject.name;
                        shopUi.gameObject.SetActive(false);
                    }




                    // 23.10.18 SSM 

                }


                for (int i = 0; i < itemCellScaleUps.Length; i++)
                {
                    if (!itemCellScaleUp.gameObject.name.Equals(itemCellScaleUps[i].gameObject.name))
                    {
                        itemCellScaleUps[i].onSizeDown();
                    }

                }

            }
        }
        else
        {
            itemCellScaleUps = FindObjectsOfType<ItemCellScaleUp>();
            for (int i = 0; i < itemCellScaleUps.Length; i++)
            {
                itemCellScaleUps[i].onSizeDown();
            }
        }
    }
    private void Build()
    {
#if BUILD_PLATFORM_PC
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭했을 때
        {
#elif TARGET_DEVICE_OCULUS
        if (ARAVR_Input.GetDown(ARAVR_Input.Button.One, ARAVR_Input.Controller.RTouch)) // 오큘러스 b버튼을 눌렀을 때
        {
#endif


#if BUILD_PLATFORM_PC
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
#elif TARGET_DEVICE_OCULUS
            Ray ray = new Ray(ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection);
            RaycastHit hit;
#endif
            prefabToPlace = Resources.Load(targetName + "_trl") as GameObject;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(targetTag))
                {
                    // 타겟 오브젝트와 부딪혔을 때 해당 위치에 프리팹을 설치
                    Collider targetCollider = hit.transform.GetComponent<Collider>();
                    if (targetCollider != null && targetCollider.enabled)
                    {

                        // 콜라이더가 활성화되어 있으면 설치

                        if (buildTurret == false)
                        {
                            GameObject tower = Instantiate(prefabToPlace, hit.point, Quaternion.identity);
                            fillAmountScript = tower.GetComponent<FillAmount>();
                            buildTurret = true;
                            targetName = "";
                        }
                        // FillAmount 스크립트의 코루틴을 시작합니다.
                        StartCoroutine(fillAmountScript.DecreaseFillAmountOverTime());
                    }
                }
            }
        }
        else
        {
#if BUILD_PLATFORM_PC
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
#elif TARGET_DEVICE_OCULUS
            Ray ray = new Ray(ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection);
            RaycastHit hit;
#endif

            if (buildTurret == false)
            {
                if (targetName == "100")
                {
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
                else if (targetName == "101")
                {
                    if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(targetTag))

                    {
                        previewObject2.SetActive(true);
                        previewObject2.transform.position = hit.point;
                    }
                    else
                    {
                        previewObject2.SetActive(false);
                    }
                }

            }
        }
    }
}