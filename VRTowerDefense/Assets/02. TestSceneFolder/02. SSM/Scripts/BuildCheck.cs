using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class BuildCheck : MonoBehaviour
{
    public Material spriteDiffuseMaterial; // 오브젝트의 Material을 여기에 연결합니다.
    public bool isBuildCheck = true;
    BuildTower buildTower;

    // 색상을 바꿀 때 사용할 새로운 색상을 정의합니다.
    Color newColor = new Color(1f, 0f, 0f, 0.5f); // 빨간색 예시
    Color originColor = new Color(0f,0f,1f,0.5f);


    // Start is called before the first frame update
    void Start()
    {
        buildTower = GetComponent<BuildTower>();

    }

    // Update is called once per frame
    void Update()
    {

        //if (gameObject.transform.CompareTag("NoBuildZone"))
        //{
        //    // Material의 속성을 변경하여 색상을 적용합니다.
        //    spriteDiffuseMaterial.color = newColor;
        //}
        //else
        //{
        //    Color currentColor = spriteDiffuseMaterial.color;
        //    spriteDiffuseMaterial.color = currentColor;

        //}

    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("준영아 여기에 들어가지도 못한거같아 ... 11111");
        if (other.gameObject.CompareTag("NoBuildZone"))
        {

            Debug.Log("준영아 여기에 들어가지도 못한거같아 ... ");
            //Material의 속성을 변경하여 색상을 적용합니다.
            spriteDiffuseMaterial.color = newColor;
            buildTower.buildTurret = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NoBuildZone"))
        {
            
            //Debug.LogError(spriteDiffuseMaterial.name);
            spriteDiffuseMaterial.color = originColor;
            buildTower.buildTurret = false;

        }
    }

}
