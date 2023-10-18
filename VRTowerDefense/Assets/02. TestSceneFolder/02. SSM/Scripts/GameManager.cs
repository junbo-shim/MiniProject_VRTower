using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int playerHp = 90;      // 플레이어의 체력
    public static GameManager instance;
    public TMP_Text hpText;
    // 싱글턴으로 만들기
    private int coin;
   
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
    public void AddCoin(int addNum)
    {
        coin += addNum;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpText.text = playerHp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HpMin(int dam)
    {
        playerHp -= dam;
        hpText.text = playerHp.ToString();
    }
}
