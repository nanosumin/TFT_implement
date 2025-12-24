using UnityEngine;

public class TestSpawn : MonoBehaviour
{

    //테스트 스폰이니까
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // TestSpawn.cs 내용
void Start() {
    // 게임 시작 1초 뒤에 "warrior_01" 유닛을 (0, 1, 0) 위치에 소환
    Invoke("Test", 1f);
}
void Test() {
    UnitSpawner.Instance.SpawnUnit("warrior_01", new Vector3(0, 1f, 0));
}
}

