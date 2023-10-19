using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class BuffSet : MonoBehaviour
{
    public Image image;


    
    public void Buffimge(float maxTime)
    {
        image = GetComponent<Image>();
  
        StartCoroutine(buffTimeAdd(maxTime));

    }

    private IEnumerator buffTimeAdd(float maxTime)
    {
        float time = 1;
        while (time <= maxTime)
        {
          
            time+=0.1f; 
            image.fillAmount = NormalizBuff(time, maxTime);
            yield return new WaitForSeconds(0.1f);
        }

    }

   public float NormalizBuff(float Time ,float MaxTime)
    {
        float normalizedValue = (Time - 1) / (MaxTime - 1);

        return normalizedValue;
    }
}
