using UnityEngine;
using System.Collections.Generic;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance;
    
    [Header("Data & Prefabs")]
    public TextAsset unitJsonData;      // JSON 파일을 연결할 곳
    public GameObject unitBasePrefab;   // 유닛의 기본 형태 프리팹

    private Dictionary<string, UnitData> unitLibrary = new Dictionary<string, UnitData>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        LoadUnitData();
    }

    void LoadUnitData()
    {
        if (unitJsonData == null)
        {
            Debug.LogError("JSON 파일이 연결되지 않았습니다!");
            return;
        }

        // JSON 파싱
        UnitDataList dataList = JsonUtility.FromJson<UnitDataList>(unitJsonData.text);
        foreach (UnitData data in dataList.units)
        {
            unitLibrary.Add(data.id, data);
        }
        Debug.Log("유닛 데이터 로드 완료: " + unitLibrary.Count + "종");
    }

    // 유닛을 소환할 때 쓰는 함수
    public GameObject SpawnUnit(string unitId, Vector3 spawnPos)
    {
        if (!unitLibrary.ContainsKey(unitId))
        {
            Debug.LogWarning("ID가 존재하지 않습니다: " + unitId);
            return null;
        }

        UnitData data = unitLibrary[unitId];
        GameObject newUnit = Instantiate(unitBasePrefab, spawnPos, Quaternion.identity);
        
        // 유닛에 데이터 주입 
        // 유닛 ai가 이 데이터를 사용하도록 설정
        UnitAI ai = newUnit.GetComponent<UnitAI>();
        if (ai != null)
        {
            ai.unitData = data;
        }

        return newUnit;
    }
}