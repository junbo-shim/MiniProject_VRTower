using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BntScripts : MonoBehaviour
{
    Button _startBnt; // 버튼 
    Button _exitBnt;
    public TextMeshPro TimeTxt;
    float startTime;
    void Start()
    {
        startTime = Time.time;
        if (gameObject.name.Equals("StartBnt"))
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
    void Update()
    {

    }
    //시작
    public void StartBnt()
    {

         SceneManager.LoadScene("MainScene");
    }
    //종료
    public void ExitBnt()
    {

        Application.Quit();
    }
}
