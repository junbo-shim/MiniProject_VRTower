using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopTF : MonoBehaviour
{

    public void ShopActiveTF()
    {
       
        AddShopList addShop = FindObjectOfType<AddShopList>();

        for(int i = 0; i<addShop.transform.childCount; i++)
        {
          
            GameObject shopChild  = addShop.transform.GetChild(i).gameObject;
            shopChild.GetComponent<ItemCellScaleUp>().BuyPossible();

            string shopChildName = addShop.transform.GetChild(i).gameObject.name;
            int gold = int.Parse(shopChild.transform.Find("Item_Information").Find("BntBuy").Find("BuyText").GetComponent<TMP_Text>().text.ToString());
            string toTal = shopChild.transform.Find("Explanation").transform.Find("Quantity_Text").transform.GetComponent<TMP_Text>().text.ToString();

            string[] a =  toTal.Split("/");
            toTal = a[1].ToString();
           
            Debug.Log(toTal);
            int input = int.Parse(toTal);
           


       

            if (gold > GameManager.instance.coin)
            {
                shopChild.GetComponent<ItemCellScaleUp>().MaxItem();
             
            }
            TurretShoot[] TurretShootList = FindObjectsOfType<TurretShoot>();
            int TurretCount = 0;
            for (int j = 0; j < TurretShootList.Length; j++)
            {
                if (TurretShootList[j].gameObject.name.Contains(shopChildName))
                {
                    TurretCount++;
                }
              
            }
        
          
            shopChild.transform.Find("Explanation").transform.Find("Quantity_Text").transform.GetComponent<TMP_Text>().text =  TurretCount.ToString() + " / " + input.ToString() ;
           if (input <= TurretCount)
            {
                shopChild.GetComponent<ItemCellScaleUp>().MaxItem();
            }

        }
    }
}
