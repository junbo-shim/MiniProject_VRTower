using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddShopList : MonoBehaviour
{
    public GameObject shopList; // 유닛 프리팹
    public string[] explanation_Texts;// 추가할 유닛 설명 
    public int[] buyCoins; // 구매 금액
    public int[] times; // 시간초
    public Sprite[] itemImg;
    private List<GameObject> shopObjList; // 추가한 샵리스트
    
    // Start is called before the first frame update
    void Start()
    {
        shopObjList = new List<GameObject>();
        for (int i = 0; i < explanation_Texts.Length; i++)
        {
            shopObjList.Add(Instantiate( shopList));
        }
        //리스트 추가 함수
        ShopItemListAdd();
        for(int i = 0; i < explanation_Texts.Length; i++)
        {
            TMP_Text explanation = shopObjList[i].transform.Find("Explanation").transform.Find("Explanation_Text").transform.GetComponent<TMP_Text>();
            Debug.Log(explanation.text);
        }
        //상점 아이템 추가
        ShopItemAdd();
        
    }

    //상점 리시트 추가
    void ShopItemListAdd()
    {
        for (int i = 0; i < explanation_Texts.Length; i++)// 추
        {
            

            Debug.Log(shopObjList[i].name);
            //{설명 텍스트 추가
            TMP_Text explanation = shopObjList[i].transform.Find("Explanation").transform.Find("Explanation_Text").transform.GetComponent<TMP_Text>();   
            explanation.text = explanation_Texts[i];
            //}설명 텍스트 
         
            //{시간 텍스트 추가
            TMP_Text Time = shopObjList[i].transform.Find("Item_Information").Find("TimeIcon").Find("TimeText").GetComponent<TMP_Text>();
            Time.text = times[i].ToString();
            //}시간 텍스트 추가

            //{구매 텍스트 추가 
            TMP_Text Buy = shopObjList[i].transform.Find("Item_Information").Find("BntBuy").Find("BuyText").GetComponent<TMP_Text>();
            Buy.text = buyCoins[i].ToString();
            //}구매 텍스트 

            //{시간 텍스트 추가
            Image img = shopObjList[i].transform.Find("Item_Information").Find("TurretImg").GetComponent<Image>();
            img.sprite = itemImg[i];
            //}시간 텍스트 추가

            shopObjList[i].name = img.sprite.name;
        }

    }
    //상점 아이템 추가
    void ShopItemAdd()
    {
        
        for (int i = 0; i < shopObjList.Count; i++)
        {
            //TMP_Text explanation = shopObjList[i].transform.Find("Explanation").transform.Find("Explanation_Text").transform.GetComponent<TMP_Text>();
          

            GameObject newObject = Instantiate(shopObjList[i]);
            newObject.transform.SetParent(transform);

            RectTransform newObjcetRT = newObject.GetComponent<RectTransform>();


            // 위치값 수정
            newObjcetRT.anchoredPosition3D = new Vector3(
                newObjcetRT.transform.position.x, newObjcetRT.transform.position.y, 0f);


            newObjcetRT.localScale = new Vector3(0.8f, 0.8f, 1f);
            
        }
    }
  
}
