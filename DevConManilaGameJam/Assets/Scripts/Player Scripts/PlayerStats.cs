using TMPro;
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

    [Header("Stats Info Text")]
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI healthRegenText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI critDamageText;
    public TextMeshProUGUI critRateText;
    public TextMeshProUGUI attackSpeedText;

    public PlayerAttack playerAttack;
    public ShopManager shop;

    public float hpToRegen = 0;
    public bool isPlayerDead = false;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        shop = FindAnyObjectByType<ShopManager>();
        InitialValue();
        UpdateStats();
    }

    public void TakeDamage(float damage)
    {
        if (!isPlayerDead)
        {
            currentHealth -= damage;
        }
        if (currentHealth < 0)
        {
            isPlayerDead = true;
            currentHealth = 0;
        }

    }

    public void RegenHP()
    {
        if (!isPlayerDead)
        {
            hpToRegen += healthRegen * Time.deltaTime;
            if (hpToRegen >= 1)
            {
                currentHealth++;
                hpToRegen--;
            }
        }
    }

    #region Stats Calculation
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
        HealthCalculation();
        RegenCalculation();
        DamageCalculation();
        CritDamageCalculation();
        CritRateCalculation();
        AttackSpeedCalculation();

        playerAttack.fireRate = attackSpeed;
        playerAttack.damage = damage;
        playerAttack.critRate = critRate;
        playerAttack.critDamage = critDamage;

        maxHealthText.text = $"Health: {maxHealth}";
        healthRegenText.text = $"HP Regen: {healthRegen:F2}/s";
        damageText.text = $"Damage: {damage:F1}";
        critDamageText.text = $"Crit Dmg: {critDamage}";
        critRateText.text = $"Crit Rate: {critRate}%";
        attackSpeedText.text = $"Atk Spd: {attackSpeed:F3}/s";
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
    #endregion
}
