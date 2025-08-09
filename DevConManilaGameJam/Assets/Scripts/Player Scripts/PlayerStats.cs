using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float healthRegen;
    public float damage;
    public float critDamage;

    public float critRate;
    public float attackSpeed;

    [Header("Scale")]
    public float maxHealthScale;
    public float healthRegenScale;
    public float damageScale;
    public float critDamageScale;

    public float critRateScale;
    public float attackSpeedScale;

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
        RegenCalculation();
        DamageCalculation();
        CritDamageCalculation();
        CritRateCalculation();
        AttackSpeedCalculation();
    }

    public void HealthCalculation()
    {
        float stat = 20;
        for (int i = 1; i < shop.healthLevel; i++)
        {
            stat += Mathf.Round(stat * maxHealthScale);
        }
        maxHealth = stat;
    }

    public void RegenCalculation()
    {
        float stat = 1;
        for (int i = 1; i < shop.healthRegenLevel; i++)
        {
            stat += Mathf.Round(stat * healthRegenScale * 100f) / 100f;
        }
        healthRegen = stat;
    }

    public void DamageCalculation()
    {
        float stat = 1;
        for (int i = 1; i < shop.damageLevel; i++)
        {
            stat += Mathf.Round(stat * damageScale * 10) / 10;
        }
        damage = stat;
    }

    public void CritDamageCalculation()
    {
        float stat = 0;
        for (int i = 1; i < shop.critDamageLevel; i++)
        {
            stat += critDamageScale;
        }
        critDamage = stat;
    }

    public void CritRateCalculation()
    {
        float stat = 0;
        for (int i = 1; i < shop.critRateLevel; i++)
        {
            stat += critRateScale;
        }
        critRate = stat;
    }

    public void AttackSpeedCalculation()
    {
        float stat = 0.5f;
        for (int i = 1; i < shop.attackSpeedLevel; i++)
        {
            stat -= attackSpeedScale;
        }
        attackSpeed = stat;
    }
}
