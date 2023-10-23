using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class BuildCheck : MonoBehaviour
{
    public Material spriteDiffuseMaterial; // 오브젝트의 Material을 여기에 연결합니다.


    // 색상을 바꿀 때 사용할 새로운 색상을 정의합니다.
    Color newColor = new Color(225f, 0f, 21f , 47f); // 빨간색 예시

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.transform.CompareTag("NoBuildZone"))
        {
            // Material의 속성을 변경하여 색상을 적용합니다.
            spriteDiffuseMaterial.color = newColor;
        }
        else
        {
            Color currentColor = spriteDiffuseMaterial.color;
            spriteDiffuseMaterial.color = currentColor;

        }

    }


}
