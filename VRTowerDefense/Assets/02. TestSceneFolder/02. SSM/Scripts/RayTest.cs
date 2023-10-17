using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayTest : MonoBehaviour
{
 
    public float raycastDistance = 100f;
    public LayerMask uiLayer;
    ItemCellScaleUp[] itemCellScaleUps;
    ItemCellScaleUp itemCellScaleUp;
    public void Start()
    {
        itemCellScaleUps = FindObjectsOfType<ItemCellScaleUp>();
    }
    void Update()
    {
        // 마우스 위치로 레이 생성
        RaycastHit hit;

        Ray ray = new (ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection);
        //Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
           
         
            if(hit.collider.tag.Equals("Item"))
            {
                itemCellScaleUp = hit.collider.gameObject.GetComponent<ItemCellScaleUp>();
               

                itemCellScaleUp.onSizeUp();
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
            for (int i = 0; i < itemCellScaleUps.Length; i++)
            {
                itemCellScaleUps[i].onSizeDown();
            }
        }

    }
}
