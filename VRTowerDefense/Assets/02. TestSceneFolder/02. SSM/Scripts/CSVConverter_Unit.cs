using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class CSVConverter_Unit : MonoBehaviour
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
    public UnitDataScriptableObject unitDataScriptableObject; // Inspector에서 할당


    // { csv 파일 데이터 변수
    public int[] CSVID { get; private set; }
    public string[] Description { get; private set; }
    public string[] Type { get; private set; }
    public int[] Duration { get; private set; }
    public float[] Firing_Interval { get; private set; }
    // } csv 파일 데이터 변수
    public string[] Recognition { get; private set; } // 시간추가
 
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
        List<UnitData> unitDataDataList = new List<UnitData>();
        for(int i= 0; i < CSVID.Length-1; i++)
        {

            UnitData item = new UnitData
            {
                CSVID = CSVID[i],
                Description = Description[i],
                Type = Type[i],
                Duration = Duration[i],
                Firing_Interval = Firing_Interval[i],
                Recognition = Recognition[i],
            
            };

            unitDataDataList.Add(item);
        }

        // CSVID를 기준으로 오름차순 정렬
        List<UnitData> unitDatasortList = unitDataDataList.OrderBy(item => item.CSVID).ToList();
        unitDataScriptableObject.items = unitDatasortList;

       
    }
 

 
    private void ReadCSV()
    {
        itemDataFile = Resources.Load<TextAsset>("CSVData/Unit");//여역시
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
        Type = new string[csvRowCount];
        Duration = new int[csvRowCount];
        Firing_Interval = new float[csvRowCount];
        Recognition = new string[csvRowCount];
    
    }

    private void SortData() 
    {
        // TODO : 매직넘버 없애기
        for (int i = 0; i < csvRowCount - 1; i++)
        {
            CSVID[i] = int.Parse(itemDataList[csvColumnCount * (i + 1)]);
            Description[i] = itemDataList[(csvColumnCount * (i + 1)) + 1];
            Type[i] = itemDataList[(csvColumnCount * (i + 1)) + 2];
            Duration[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 3]);
            Firing_Interval[i] = float.Parse(itemDataList[(csvColumnCount * (i + 1)) + 4]);
            Recognition[i] = itemDataList[(csvColumnCount * (i + 1)) + 5];
       
        }
    }

 
}
