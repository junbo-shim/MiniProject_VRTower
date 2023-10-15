using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
public class CSVConverter_Shop : MonoBehaviour
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
    public ItemDataScriptableObject itemDataScriptableObject; // Inspector에서 할당


    // { csv 파일 데이터 변수
    public int[] CSVID { get; private set; }
    public string[] Description { get; private set; }
    public int[] UnitID { get; private set; }
    public int[] Price { get; private set; }
    public int[] TotalNum { get; private set; }
    // } csv 파일 데이터 변수


    private void Awake()
    {

        ReadCSV();
        CheckCSVRowColumn();

        itemDataList = new string[csvRowCount * csvColumnCount];

        SplitData();
        InitArrays();

        SortData();
        itemDataListCopy();
       // Test();
    }

    private void itemDataListCopy()
    {
        List<ItemData> ShopItemDataList = new List<ItemData>();
        for(int i= 0; i < CSVID.Length-1; i++)
        {
          
            ItemData item = new ItemData
            {
                CSVID = CSVID[i],
                Description = Description[i],
                UnitID = UnitID[i],
                Price = Price[i],
                TotalNum = TotalNum[i]
            };

            ShopItemDataList.Add(item);
        }
        itemDataScriptableObject.items = ShopItemDataList;

       
    }
 

 
    private void ReadCSV()
    {
        itemDataFile = Resources.Load<TextAsset>("CSVData/test");//여역시
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
        UnitID = new int[csvRowCount];
        Price = new int[csvRowCount];
        TotalNum = new int[csvRowCount];
    }

    private void SortData() 
    {
        // TODO : 매직넘버 없애기
        for (int i = 0; i < csvRowCount - 1; i++)
        {
            CSVID[i] = int.Parse(itemDataList[csvColumnCount * (i + 1)]);
            Description[i] = itemDataList[(csvColumnCount * (i + 1)) + 1];
            UnitID[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 2]);
            Price[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 3]);
            TotalNum[i] = int.Parse(itemDataList[(csvColumnCount * (i + 1)) + 4]);
        }
    }

    private void Test() 
    {
        for (int i = 0; i < csvRowCount - 1; i++) 
        {
            Debug.Log(CSVID[i]);
            Debug.Log(Description[i]);
            Debug.Log(UnitID[i]);
            Debug.Log(Price[i]);
            Debug.Log(TotalNum[i]);
        }
    }
}
