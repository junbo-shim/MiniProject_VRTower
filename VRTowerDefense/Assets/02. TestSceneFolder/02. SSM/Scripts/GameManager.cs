using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int playerHp = 90;      // 플레이어의 체력
    public static GameManager instance;
    public TMP_Text hpText;
    public TMP_Text coinText;
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
    }
    public void MinCoin(int minNum)
    {
        coin -= minNum;
        coinText.text = coin.ToString();
    }
    public void HpMin(int dam)
    {
        playerHp -= dam;
        hpText.text = playerHp.ToString();
    }
}
