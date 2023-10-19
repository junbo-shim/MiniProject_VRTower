using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitFlash : MonoBehaviour
{
  



    private Image image;
    private bool isFlashing = false;

    void Start()
    {
        image = GetComponent<Image>();
   
        StartFlash(0.1f);
    }

    // 피격 플래시를 시작합니다.
    public void StartFlash(float flashDuration)
    {
        if (!isFlashing)
        {
            isFlashing = true;
            StartCoroutine(FlashRoutine(flashDuration));
        }
    }
    public int Alpha;

    // 피격 플래시가 종료됩니다.
    private void StopFlash()
    {
        isFlashing = false;
        Alpha = 250;
        gameObject.SetActive(false);
    }
   
    // 피격 플래시 루틴
    private IEnumerator FlashRoutine(float flashDuration)
    {
        Alpha = 250;
  
        image.color = new Color(image.color.r, image.color.g, image.color.b, Alpha/255);
        while (Alpha >= 0)
        {
           
            image.color = new Color(image.color.r, image.color.g, image.color.b, Alpha/255.0f);
            Alpha -= 25;
            yield return new WaitForSeconds(flashDuration);
        }
     
        StopFlash();
    }
}
