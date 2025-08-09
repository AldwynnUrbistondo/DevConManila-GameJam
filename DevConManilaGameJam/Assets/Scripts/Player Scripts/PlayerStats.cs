using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float healthRegen;
    public float damage;
    public float critDamage;

    public float critRate;
    public float attackSpeed;

    public PlayerAttack playerAttack;
    public ShopManager shop;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        shop = FindAnyObjectByType<ShopManager>();
        InitialValue();
        UpdateStats();
    }


    private void InitialValue()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
        healthRegen = 1;
        damage = 1;
        critDamage = 0;
        critRate = 0;
        attackSpeed = 0.5f;

    }

    public void UpdateStats()
    {

        playerAttack.fireRate = attackSpeed;
        playerAttack.damage = damage;
        playerAttack.critRate = critRate;
        playerAttack.critDamage = critDamage;

        HealthCalculation();
    }

    public void HealthCalculation()
    {
        float stat = 20;
        for (int i = 0; i < shop.healthLevel; i++)
        {
            stat += Mathf.Round(stat * 0.05f);
        }
        maxHealth = stat;
    }
}
