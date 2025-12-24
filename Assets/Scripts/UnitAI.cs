using UnityEngine;

public class UnitAI : MonoBehaviour
{
    public UnitData unitData; // Spawner가 넣어줄 데이터
    
    private Health health;
    private Mana mana;
    private Transform target;

    void Start()
    {
        health = GetComponent<Health>();
        mana = GetComponent<Mana>();

        // 데이터가 있다면 초기 세팅
        if (unitData != null)
        {
            health.Setup(unitData.hp);
        }
    }

    void Update()
    {
        //전투 로직 넣을 예정
    }

    public void Tick()
    {
        // 전투 중 매 틱마다 할 일
    }
}