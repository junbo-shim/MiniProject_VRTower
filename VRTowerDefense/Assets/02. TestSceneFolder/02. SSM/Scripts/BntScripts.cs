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
        Ray ray = new Ray(ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection);
        RaycastHit hit;
        Debug.DrawRay(ARAVR_Input.RHandPosition, ARAVR_Input.RHandDirection * 300, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name.Equals("StartBnt"))
            {
                Debug.Log("1");
                if (ARAVR_Input.GetDown(ARAVR_Input.Button.One, ARAVR_Input.Controller.RTouch)) // 오큘러스 b버튼을 눌렀을 때
                {
                    Debug.Log("2");
                    StartBnt();
                }
            }
            if (hit.collider.name.Equals("ExitBnt"))
            {
                if (ARAVR_Input.GetDown(ARAVR_Input.Button.One, ARAVR_Input.Controller.RTouch)) // 오큘러스 b버튼을 눌렀을 때
                {
                    ExitBnt();
                }
            }

        }
    }
    //시작
    public void StartBnt()
    {

         SceneManager.LoadScene("MainScene_ssm");
    }
    //종료
    public void ExitBnt()
    {

        Application.Quit();
    }
}
