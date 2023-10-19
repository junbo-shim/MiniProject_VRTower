using UnityEngine;
using UnityEngine.UI;
public  class ItemCellScaleUp : MonoBehaviour
{

    private Image infoImage;
    private Image explanImage;
    private Color org1;
    private Color org2;

    void Start()
    {
        infoImage = transform.Find("Item_Information").GetComponent<Image>();
        explanImage = transform.Find("Explanation").GetComponent<Image>();
        org1 = infoImage.color;
        org2 = explanImage.color;
    }

    public void onSizeUp()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
     
    
    }
    public void onSizeDown()
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
   
    }
    public void MaxItem()
    {
        infoImage = transform.Find("Item_Information").GetComponent<Image>();
        explanImage = transform.Find("Explanation").GetComponent<Image>();
   
        infoImage.color = new Color(0f, 0f, 0f);
        explanImage.color = new Color(0f, 0f, 0f);
    }
    public void BuyPossible()
    {
        infoImage = transform.Find("Item_Information").GetComponent<Image>();
        explanImage = transform.Find("Explanation").GetComponent<Image>();
        infoImage.color = org1;
        explanImage.color = org2;
    }

    public void onChackDown()
    {
        Debug.Log(gameObject.name);
    }



}
