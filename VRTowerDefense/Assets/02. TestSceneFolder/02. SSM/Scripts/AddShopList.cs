using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddShopList : MonoBehaviour
{
    public GameObject shopList; // 유닛 프리팹

    private int[] csvID_Shop; // 순서
    private string[] description_Shop;// 추가할 유닛 설명 
    private int[] unitID_Shop; // 유닛아이디
    private int[] price_Shop; // 구매 금액
    private int[] TotalNum;  // 구매 가능 수량

    private int[] times; // 시간초 상의




    private List<GameObject> shopObjList; // 추가한 샵리스트
    public ItemDataScriptableObject itemDataScriptableObject; // Inspector에서 할당

    // Start is called before the first frame update
    void Start()
    {
        // 배열초기화 없으면 오류
        csvID_Shop = new int[itemDataScriptableObject.items.Count];
        description_Shop = new string[itemDataScriptableObject.items.Count];
        unitID_Shop = new int[itemDataScriptableObject.items.Count];
        price_Shop = new int[itemDataScriptableObject.items.Count];
        TotalNum = new int[itemDataScriptableObject.items.Count];
        // 배열초기화 없으면 오류

        int csvCount = 0;
        // ScriptableObject데이터 배열에 추가
        foreach (var item in itemDataScriptableObject.items)
        {
            csvID_Shop[csvCount] = item.CSVID;
            description_Shop[csvCount] = item.Description;
            unitID_Shop[csvCount] = item.UnitID;
            price_Shop[csvCount] = item.Price;
            TotalNum[csvCount] = item.TotalNum;
            csvCount++;
            Debug.Log("CSVID: " + item.CSVID + ", Description: " + item.Description + ", UnitID: " + item.UnitID);
        }
        //  ScriptableObject데이터 배열에 추가


        shopObjList = new List<GameObject>();
        for (int i = 0; i < csvID_Shop.Length; i++)
        {
            shopObjList.Add(Instantiate(shopList));
        }
        //리스트 추가 함수
        ShopItemListAdd();

        //상점 아이템 추가
        ShopItemAdd();

    }
    //이미지 가져오기
    public Sprite LoadSprite(string imagePath)
    {
        // 이미지 파일을 Resources.Load로 로드
        Sprite sprite = Resources.Load<Sprite>("Unit/"+imagePath);


        return sprite;
    }


    //상점 리시트 추가
    void ShopItemListAdd()
    {
        for (int i = 0; i < csvID_Shop.Length; i++)// 추
        {

           
                //{설명 텍스트 추가
                TMP_Text explanation = shopObjList[i].transform.Find("Explanation").transform.Find("Explanation_Text").transform.GetComponent<TMP_Text>();
                explanation.text = description_Shop[i];
                //}설명 텍스트 

                //{시간 텍스트 추가
                TMP_Text Time = shopObjList[i].transform.Find("Item_Information").Find("TimeIcon").Find("TimeText").GetComponent<TMP_Text>();
                Time.text = TotalNum[i].ToString(); // 상의 필요
                                                    //}시간 텍스트 추가

                //{구매 텍스트 추가 
                TMP_Text Buy = shopObjList[i].transform.Find("Item_Information").Find("BntBuy").Find("BuyText").GetComponent<TMP_Text>();
                Buy.text = price_Shop[i].ToString();
                //}구매 텍스트 

                //{구매 가능 총량 텍스트 추가
                TMP_Text quantity = shopObjList[i].transform.Find("Explanation").transform.Find("Quantity_Text").transform.GetComponent<TMP_Text>();
                if (!TotalNum[i].Equals(null))
                {
                    quantity.text = TotalNum[i].ToString() + " \\ ";
                }

                //}구매 가능 총량 텍스트 추가

                //{이미지 추가
                Image img = shopObjList[i].transform.Find("Item_Information").Find("TurretImg").GetComponent<Image>();
                img.sprite = LoadSprite(unitID_Shop[i].ToString());
                //}이미지 추가
          
        }

    }
    //상점 아이템 추가
    void ShopItemAdd()
    {

        for (int i = 0; i < shopObjList.Count; i++)
        {
            //TMP_Text explanation = shopObjList[i].transform.Find("Explanation").transform.Find("Explanation_Text").transform.GetComponent<TMP_Text>();


            GameObject newObject = shopObjList[i];
            newObject.transform.SetParent(transform);

            RectTransform newObjcetRT = newObject.GetComponent<RectTransform>();


            // 위치값 수정
            newObjcetRT.anchoredPosition3D = new Vector3(
                newObjcetRT.transform.position.x, newObjcetRT.transform.position.y, 0f);


            newObjcetRT.localScale = new Vector3(0.8f, 0.8f, 1f);

        }
    }

}
