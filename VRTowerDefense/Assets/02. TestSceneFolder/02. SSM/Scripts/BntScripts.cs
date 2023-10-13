using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BntScripts : MonoBehaviour
{
    Button _startBnt; // 버튼 
    Button _exitBnt;
    void Start()
    {
       
        if(gameObject.name.Equals("StartBnt"))
        {
            //버튼 찾아서 연결해주기
            _startBnt = GetComponent<Button>();

            //AddListener로 startBnt 함수 연결
            _startBnt.onClick.AddListener(StartBnt);
        }
        else
        {
            //버튼 찾아서 연결해주기
            _exitBnt = GetComponent<Button>();

            //AddListener로 ExitBnt 함수 연결
            _exitBnt.onClick.AddListener(ExitBnt);
        }
   
    }
    //시작
    public void StartBnt()
    {
    
        //  SceneManager.LoadScene("");
    }
    //종료
    public void ExitBnt()
    {

        Application.Quit();
    }
}
