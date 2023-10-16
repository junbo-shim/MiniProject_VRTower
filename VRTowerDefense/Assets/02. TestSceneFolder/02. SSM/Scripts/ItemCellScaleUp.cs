using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class ItemCellScaleUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
    }
}
