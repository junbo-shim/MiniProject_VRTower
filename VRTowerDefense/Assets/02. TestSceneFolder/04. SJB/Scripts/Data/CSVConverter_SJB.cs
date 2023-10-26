using System.Collections.Generic;
using UnityEngine;

public class CSVConverter_SJB : MonoBehaviour
{
    #region Singleton
    private static CSVConverter_SJB converter_Instance;
    // 싱글턴으로 만들기
    public static CSVConverter_SJB Instance
    {
        get
        {
            if (converter_Instance == null)
            {
                converter_Instance = FindObjectOfType<CSVConverter_SJB>();
            }
            return converter_Instance;
        }
    }
    #endregion

    #region 프로퍼티 및 변수
    // 읽어온 TextAsset 파일의 정보를 담을 변수
    private TextAsset itemDataFile = default;
    // 데이터 파일을 쪼개서 담을 배열 (csv 한 칸에 해당)
    public string[] ItemDataList { get; private set; }
    // csv 파일 행 크기
    public int CSVRowCount { get; private set; }
    // csv 파일 열 크기
    public int CSVColumnCount { get; private set; }
    // csv 파일에서 읽어낸 ID 값을 Key 로 하는 Dictionary (Key : ID_int, Value : Scriptable Object_Class)
    public Dictionary<int, ScriptableObject> ScriptableObjDictionary { get; private set; }
    // ScriptableObjDictionary 의 key 변수
    private int key;
    // switch case 문을 통해 분류할 number 변수
    private int num;
    // scriptable object : Boss
    public ScriptableObj_Boss bossData { get; private set; }
    // scriptable object : Minion
    public ScriptableObj_Minion minionData { get; private set; }
    // scriptable object : Projectile
    public ScriptableObj_Projectile projectileData { get; private set; }
    #endregion


    private void Awake()
    {
        ReadCSV();
        //Test();
    }


    #region 메서드
    // csv 파일을 읽어오는 함수
    private void ReadCSV()
    {
        // csvs 배열에 Resources/CSVData 폴더 안의 모든 TextAsset 을 저장한다
        TextAsset[] csvs = Resources.LoadAll<TextAsset>("CSVData_SJB");
        // Dictionary 를 초기화 한다
        ScriptableObjDictionary = new Dictionary<int, ScriptableObject>();

        // csvs 배열크기만큼 반복문을 실행한다
        // Resources/CSVData 폴더 안의 CSV 파일 수만큼 반복한다
        for (int i = 0; i < csvs.Length; i++) 
        {
            // itemDataFile 변수에 특정 배열 요소를 저장한다
            // 반복문을 돌 때마다 csv 파일을 바꿔서 저장한다
            itemDataFile = csvs[i];
            // itemDataFile 에 저장한 csv 파일을 기준으로 함수를 실행한다
            CheckAndSaveID();
        }
    }

    // csv 파일에서 ID 값만 추출해서 int 로 변환, List 에 저장하는 함수
    private void CheckAndSaveID()
    {
        // { itemDataList 가로 세로 저장하기
        string dataTrimNull = itemDataFile.text.TrimEnd();
        string[] tempRow = default;
        string[] tempColumn = default;
        string[] splitdata = default;

        tempRow = dataTrimNull.Split("\n");
        CSVRowCount = tempRow.Length;

        tempColumn = tempRow[0].Split(",");
        CSVColumnCount = tempColumn.Length;
        // } itemDataList 가로 세로 저장하기


        // { ID 값만 얻어오기 위해서 \n와 , 로 데이터 한 칸씩만 남기기
        dataTrimNull = itemDataFile.text.TrimEnd();
        splitdata = dataTrimNull.Split(new char[] { '\n', ',' });
        // } ID 값만 얻어오기

        //Debug.LogWarning(dataTrimNull);

        // { ID 값을 Dictionary 에 저장하기
        for (int i = CSVColumnCount; i < splitdata.Length; i += CSVColumnCount)
        {
            // ID 값에서 뒷 3자리를 없애고 어떤 것에 대한 데이터인지 판별할 변수
            num = default;
            // ID 값에서 뒷 3자리를 없애주는 함수의 결과값을 number 변수에 저장
            num = DropLastThree(splitdata[i]);

            // ScriptableObjDictionary 에 저장할 Key : 1001, 2001, 3001, 3002 etc
            key = int.Parse(splitdata[i]);


            // ScriptableObjDictionary 에 저장할 Value 초기화
            ScriptableObject createdScriptableObj = default;

            // ID 값에서 뒷 3자리를 없앤 수가 만약 1 ~ 3 인 경우
            // 경우에 따라서 다른 데이터를 형태에 맡게 Scriptable Object 로 만든다
            switch (num)
            {
                case 1:
                    createdScriptableObj =
                        CreateBossScriptable(dataTrimNull, CSVColumnCount, CSVRowCount);
                    break;

                case 2:
                    createdScriptableObj =
                        CreateProjectileScriptable(dataTrimNull, CSVColumnCount, CSVRowCount);
                    break;

                case 3:
                    createdScriptableObj =
                        CreateMinionScriptable(dataTrimNull, CSVColumnCount, CSVRowCount);
                    break;
            }

            // Key 값과 Value 값을 저장한다
            ScriptableObjDictionary.Add(key, createdScriptableObj);
            // createdScriptableObj 를 gameObject 에 붙인다

            // } ID 값을 Dictionary 에 저장하기
        }
    }

    // 뒷 3 자리 없애오는 함수 : ID 의 앞자리로 어떤 요소인지 판단
    private int DropLastThree(string number) 
    {
        string dropped = number.Substring(0, number.Length - 3);
        return int.Parse(dropped);
    }

    // csv 파일에서 읽어온 데이터를 사용하여, ScriptableObj_Boss 를 생성하는 함수
    private ScriptableObj_Boss CreateBossScriptable(string csvData, int column, int row) 
    {
        string[] splitData = default;

        splitData = csvData.Split(new char[] { '\n', ',' });

        for (int i = 0; i < row - 1; i++) 
        {
            int idx = (1 + i) * column;

            bossData = (ScriptableObj_Boss)ScriptableObject.CreateInstance("ScriptableObj_Boss");

            bossData.id = int.Parse(splitData[idx + 0]);
            bossData.description = splitData[idx + 1];
            bossData.hp = int.Parse(splitData[idx + 2]);
            bossData.moveSpeed = float.Parse(splitData[idx + 3]);
            bossData.atkCoolTime = float.Parse(splitData[idx + 4]);
            bossData.weakPointSize = int.Parse(splitData[idx + 5]);
            bossData.weakPointMultiflier = float.Parse(splitData[idx + 6]);
            bossData.weakPointDuration = int.Parse(splitData[idx + 7]);
        }

        

        return bossData;
    }

    // csv 파일에서 읽어온 데이터를 사용하여, ScriptableObj_Minion 를 생성하는 함수
    private ScriptableObj_Minion CreateMinionScriptable(string csvData, int column, int row)
    {
        string[] splitData = default;

        splitData = csvData.Split(new char[] { '\n', ',' });

        for (int i = 0; i < row - 1; i++)
        {
            int idx = (1 + i) * column;

            minionData = (ScriptableObj_Minion)ScriptableObject.CreateInstance("ScriptableObj_Minion");

            minionData.id = int.Parse(splitData[idx + 0]);
            minionData.description = splitData[idx + 1];
            minionData.modelName = splitData[idx + 2];
            minionData.hp = int.Parse(splitData[idx + 3]);
            minionData.damage= int.Parse(splitData[idx + 4]);
            minionData.moveSpeed = float.Parse(splitData[idx + 5]);
            minionData.agroArea = int.Parse(splitData[idx + 6]);
            minionData.explosionArea = int.Parse(splitData[idx + 7]);
        }

        return minionData;
    }

    // csv 파일에서 읽어온 데이터를 사용하여, ScriptableObj_Projectile 를 생성하는 함수
    private ScriptableObj_Projectile CreateProjectileScriptable(string csvData, int column, int row)
    {
        string[] splitData = default;

        splitData = csvData.Split(new char[] { '\n', ',' });

        for (int i = 0; i < row - 1; i++)
        {
            int idx = (1 + i) * column;

            projectileData = (ScriptableObj_Projectile)ScriptableObject.CreateInstance("ScriptableObj_Projectile");

            projectileData.id = int.Parse(splitData[idx + 0]);
            projectileData.description = splitData[idx + 1];
            projectileData.modelName = splitData[idx + 2];
            projectileData.hp = int.Parse(splitData[idx + 3]);
            projectileData.minSpeed = float.Parse(splitData[idx + 4]);
            projectileData.maxSpeed = float.Parse(splitData[idx + 5]);
            projectileData.damage = int.Parse(splitData[idx + 6]);
            projectileData.coolTime = float.Parse(splitData[idx + 7]);
            projectileData.respawnNumber = int.Parse(splitData[idx + 8]);
        }

        return projectileData;
    }
    #endregion


    private void Test() 
    {
        foreach (var a in ScriptableObjDictionary)
        {
            int b = a.Key;
            ScriptableObject c = a.Value;
            Debug.LogWarning(b);
            Debug.LogWarning(c);
        }
    }
}
