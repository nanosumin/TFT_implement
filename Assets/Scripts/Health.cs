using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp;
    public float currentHp;

    public void Setup(float hp)
    {
        maxHp = hp;
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log(gameObject.name + " 남은 체력: " + currentHp);
        
        if (currentHp <= 0) Die();
    }

    void Die()
    {
        Debug.Log(gameObject.name + " 사망!");
        Destroy(gameObject);
    }
}