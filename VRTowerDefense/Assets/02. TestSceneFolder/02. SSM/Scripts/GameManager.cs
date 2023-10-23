using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int playerHp = 100;      // 플레이어의 체력
    public PlayerController playerController;
    public static GameManager instance;
    public TMP_Text hpText;
    public TMP_Text coinText;
    public GameObject reMain;
    public GameObject golem;
    public Material emptyMaterial;
    // 싱글턴으로 만들기
    public int coin = 100;
   
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        hpText.text = playerHp.ToString();
        coinText.text = coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddCoin(int addNum)
    {

     
        coin += addNum;
        coinText.text = coin.ToString();
    }
    public void MinCoin(int minNum)
    {
        coin -= minNum;
        coinText.text = coin.ToString();
    }
    public void HpMin(int dam)
    {
        if(playerHp > 0 )
        {
            if(playerHp - dam < 0)
            {
               
                playerHp = 0;
            }
            else
            {
                playerHp -= dam;
            }
         
        }   
       if(playerHp <= 0)
        {
            //golem.transform.position = new Vector3(0, -300, 0);
            golem.GetComponent<SkinnedMeshRenderer>().material = emptyMaterial;
            reMain.SetActive(true);
        }
           
        
        hpText.text = playerHp.ToString();
    }

    public void SetPlayerState(string state)
    {
        if(state == "idle")
        {
            playerController.SetState(playerController.idleState);
        }
        else if (state == "battle")
        {
            playerController.SetState(playerController.battleState);
        }
        else if (state == "planting")
        {
            playerController.SetState(playerController.plantingState);
        }
        else if (state == "death")
        {
            playerController.SetState(playerController.deathState);
        }
    }
}

