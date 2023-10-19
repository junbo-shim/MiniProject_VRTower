using Meta.Voice.Hub;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTimeSet : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
   
    public void unitBuffSet(string unitId,float time)
    {
        image = GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Unit/" + unitId);
        FindObjectOfType<BuffSet>().Buffimge(time);
        Destroy(gameObject, time);
    }

  
}
