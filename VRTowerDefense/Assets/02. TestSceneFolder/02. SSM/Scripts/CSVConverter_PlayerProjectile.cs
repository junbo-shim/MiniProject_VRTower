using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class CSVConverter_PlayerProjectile : MonoBehaviour
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
    public PlayerProjectileDataScriptableObject playerProjectileDataScriptableObject; // Inspector에서 할당


    // { csv 파일 데이터 변수
    public int[] CSVID { get; private set; }
    public string[] Description { get; private set; }
    public string[] Model { get; private set; }
    public float[] Atk { get; private set; }
    public int[] Critical_Percentage { get; private set; }
    // } csv 파일 데이터 변수
    public float[] Critical_Rate { get; private set; } 

    public int[] Crash { get; private set; }
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
        List<Projectile_Table> ProjectileTableataList = new List<Projectile_Table>();
        for(int i= 0; i < CSVID.Length-1; i++)
        {

            Projectile_Table item = new Projectile_Table
            {
                CSVID = CSVID[i],
                Description = Description[i],
                Model = Model[i],
                Atk = Atk[i],
                Critical_Percentage = Critical_Percentage[i],
                Critical_Rate = Critical_Rate[i],
                Crash = Crash[i],
                LifeTime = LifeTime[i]
            };

            ProjectileTableataList.Add(item);
        }

        // CSVID를 기준으로 오름차순 정렬
        List<Projectile_Table> sortedProjectileDataList = ProjectileTableataList.OrderBy(item => item.CSVID).ToList();
        playerProjectileDataScriptableObject.items = sortedProjectileDataList;

       
    }
 

 
    private void ReadCSV()
    {
        itemDataFile = Resources.Load<TextAsset>("CSVData/PlayerBullet_Table");//여역시
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
        Atk = new float[csvRowCount];
        Critical_Percentage = new int[csvRowCount];
        Critical_Rate = new float[csvRowCount];
        Crash = new int[csvRowCount];
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
            Atk[i] = float.Parse(itemDataList[(csvColumnCount * (i + 1)) + 3]);
            Critical_Percentage[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 4]);
            Critical_Rate[i] = float.Parse(itemDataList[(csvColumnCount * (i + 1)) + 5]);
            Crash[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 6]);
            LifeTime[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 7]);
        }
    }

   
}
