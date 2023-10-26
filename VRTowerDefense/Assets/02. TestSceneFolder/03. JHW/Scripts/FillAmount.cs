using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class FillAmount : MonoBehaviour
{
    public Image fillImage; // FillAmount를 가진 UI 이미지
    public float duration = 10.0f; // FillAmount를 줄일 총 시간
    private float elapsedTime = 0.0f; // 경과 시간
    public UnitDataScriptableObject unitDataScriptableObject;

    private void Awake()
    {
        // CSVReader로부터 읽어온 스크립터블 오브젝트 데이터 
        foreach (var data in unitDataScriptableObject.items)
        {
            elapsedTime = data.Duration;

        }
    }


    public IEnumerator DecreaseFillAmountOverTime()
    {
        while (elapsedTime < duration)
        {
            float fillAmount = 1.0f - (elapsedTime / duration);
            fillImage.fillAmount = fillAmount;
            elapsedTime += Time.deltaTime;
            yield return null; // 1 프레임 대기
        }


            Destroy(gameObject);
        
    }
}
