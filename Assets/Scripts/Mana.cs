using UnityEngine;

public class Mana : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana = 0f;

    public void GainMana(float amount)
    {
        currentMana += amount;
        if (currentMana >= maxMana) currentMana = maxMana;
    }

    public void UseMana()
    {
        currentMana = 0;
    }
}