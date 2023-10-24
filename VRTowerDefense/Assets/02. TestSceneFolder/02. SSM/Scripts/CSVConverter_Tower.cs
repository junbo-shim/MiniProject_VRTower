using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class CSVConverter_Tower : MonoBehaviour
{
    private static CSVConverter converter_Instance;
    // 싱글턴으로 만들기
    public static CSVConverter Instance
    {
        get
        {
            if (converter_Instance == null)
            {
                converter_Instance = FindObjectOfType<CSVConverter>();
            }
            return converter_Instance;
        }
    }


    // 읽어온 TextAsset 파일의 정보를 담을 변수
    private TextAsset itemDataFile = default;

    // 최종적으로 데이터 파일을 쪼개서 담을 배열
    public string[] itemDataList { get; private set; }

    // 외부에서 접근할 csv 파일 행 크기 (test csv: 17)
    public int csvRowCount { get; private set; }
    // 외부에서 접근할 csv 파일 열 크기 (test csv: 19)
    public int csvColumnCount { get; private set; }
    public TowerDataScriptableObject towerDataScriptableObject; // Inspector에서 할당


    // { csv 파일 데이터 변수
    public int[] CSVID { get; private set; }
    public string[] Description { get; private set; }
    public string[] Model { get; private set; }
    public int[] Atk { get; private set; }
    public float[] Movement_Speed { get; private set; }
    // } csv 파일 데이터 변수
    public int[] Debuff { get; private set; } // 시간추가
    public int[] projectile_Speed { get; private set; } // 시간추가
    public int[] LifeTime { get; private set; } // 시간추가
    private void Awake()
    {

        ReadCSV();
        CheckCSVRowColumn();

        itemDataList = new string[csvRowCount * csvColumnCount];

        SplitData();
        InitArrays();

        SortData();
        itemDataListCopy();
        //Test();
    }
    //ScriptableObject로 만들기
    private void itemDataListCopy()
    {
        List<ProjectileData> projectileDataList = new List<ProjectileData>();
        for(int i= 0; i < CSVID.Length-1; i++)
        {

            ProjectileData item = new ProjectileData
            {
                CSVID = CSVID[i],
                Description = Description[i],
                Model = Model[i],
                Atk = Atk[i],
                Movement_Speed = Movement_Speed[i],
                Debuff = Debuff[i],
                projectile_Speed = projectile_Speed[i],
                LifeTime = LifeTime[i]
            };

            projectileDataList.Add(item);
        }

        // CSVID를 기준으로 오름차순 정렬
        List<ProjectileData> projectilesortDataList = projectileDataList.OrderBy(item => item.CSVID).ToList();
        towerDataScriptableObject.items = projectilesortDataList;

       
    }
 

 
    private void ReadCSV()
    {
        itemDataFile = Resources.Load<TextAsset>("CSVData/Projectile");//여역시
    }

    private void CheckCSVRowColumn()
    {
        string dataTrimNull_ = itemDataFile.text.TrimEnd();
        string[] tempRow_ = default;
        string[] tempColumn_ = default;

        tempRow_ = dataTrimNull_.Split("\n");
        csvRowCount = tempRow_.Length;

        tempColumn_ = tempRow_[0].Split(",");
        csvColumnCount = tempColumn_.Length;
    }

    private void SplitData()
    {
        string dataTrimNull_ = itemDataFile.text.TrimEnd();
        string[] splitdata_ = default;

        splitdata_ = dataTrimNull_.Split(new char[] {'\n' , ','});


        for (int i = 0; i < csvRowCount * csvColumnCount; i++)
        {
            itemDataList[i] = splitdata_[i];
        }
    }

    private void InitArrays()
    {
        CSVID = new int[csvRowCount];
        Description = new string[csvRowCount];
        Model = new string[csvRowCount];
        Atk = new int[csvRowCount];
        Movement_Speed = new float[csvRowCount];
        Debuff = new int[csvRowCount];
        projectile_Speed = new int[csvRowCount];
        LifeTime = new int[csvRowCount];
    }

    private void SortData() 
    {
        // TODO : 매직넘버 없애기
        for (int i = 0; i < csvRowCount - 1; i++)
        {
            CSVID[i] = int.Parse(itemDataList[csvColumnCount * (i + 1)]);
            Description[i] = itemDataList[(csvColumnCount * (i + 1)) + 1];
            Model[i] = itemDataList[(csvColumnCount * (i + 1)) + 2];
            Atk[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 3]);
            Movement_Speed[i] = float.Parse(itemDataList[(csvColumnCount * (i + 1)) + 4]);
            Debuff[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 5]);
            projectile_Speed[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 6]);
            LifeTime[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 7]);
        }
    }

 
}
