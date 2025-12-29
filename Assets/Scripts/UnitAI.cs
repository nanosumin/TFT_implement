using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;



public class UnitAI : MonoBehaviour
{

    public enum UnitState { Attack, Move, Idle, Die }
    public UnitState currentState = UnitState.Idle;
    public UnitData unitData; // Spawner가 넣어줄 데이터

    private Health health;
    private Mana mana;
    private GameObject targetEnemy;
    private float lastAttackTime;


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


    void FindTarget()
    {    //아군 유닛은 적군 타겟팅 적군 유닛은 아군 타겟팅
        string targetTag = gameObject.CompareTag("MyUnit") ? "EnemyUnit" : "MyUnit";
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

        //거리 계산해서 상대랑 싸우기
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

    }
    
    void CheckRange()
    {
        if (targetEnemy == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, targetEnemy.transform.position);

        currentState = (distance <= unitData.attackRange) ? UnitState.Attack : UnitState.Move;
        
        
    }

    void MoveToTarget()
    {
        if (targetEnemy == null)
        {
            currentState = UnitState.Idle; return;
        }
        // 타겟으로 움직이기
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, unitData.moveSpeed*Time.deltaTime);
        transform.LookAt(targetEnemy.transform);  //타겟 바라보기
        //사거리 안에 들어오면 공격하기
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= unitData.attackRange)
        {
            currentState = UnitState.Attack;
        }
     
        

    }

    void AttackTarget()
    {    //적없으면 안때리기
        if (targetEnemy == null)
        {
            return;
        }
        //공속 비례 공격
        if (Time.time >= lastAttackTime + (1f/ unitData.attackSpeed))
        {
            Health enemyHealth = targetEnemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(unitData.damage);
                if (mana != null) mana.GainMana(10);
                Debug.Log($"{gameObject.name}이 {gameObject.name}공격");
            }

            lastAttackTime = Time.time;

        }
        // 사거리 밖에 있으면 이동하기!
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) > unitData.attackRange)
        {
            currentState = UnitState.Move;
        }
    }

}