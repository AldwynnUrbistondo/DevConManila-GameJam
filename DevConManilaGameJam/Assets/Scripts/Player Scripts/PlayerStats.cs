using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Initial Stats")]
    public float initialMaxHealth;
    public float initialHealthRegen;
    public float initialDamage;
    public float initialCritDamage;

    public float initialCritRate;
    public float initialAttackSpeed;

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
        //InitialValue();
        UpdateStats();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        RegenHP();
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
        if (!isPlayerDead && currentHealth < maxHealth)
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
        maxHealth = initialMaxHealth;
        currentHealth = maxHealth;
        healthRegen = initialHealthRegen;
        damage = initialDamage;
        critDamage = initialCritDamage;
        critRate = initialCritRate;
        attackSpeed = initialAttackSpeed;

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

        maxHealthText.text = $"Health:\n{maxHealth}";
        healthRegenText.text = $"HP Regen:\n{healthRegen:F2}/s";
        damageText.text = $"Damage:\n{damage:F1}";
        critDamageText.text = $"Crit Dmg:\n{critDamage}";
        critRateText.text = $"Crit Rate:\n{critRate}%";
        attackSpeedText.text = $"Atk Spd:\n{attackSpeed:F3}/s";
    }

    public void HealthCalculation()
    {
        float stat = initialMaxHealth;
        for (int i = 1; i < shop.healthLevel; i++)
        {
            stat += maxHealthScale;
        }
        maxHealth = stat;
    }

    public void RegenCalculation()
    {
        float stat = initialHealthRegen;
        for (int i = 1; i < shop.healthRegenLevel; i++)
        {
            stat += Mathf.Round(stat * healthRegenScale * 100f) / 100f;
        }
        healthRegen = stat;
    }

    public void DamageCalculation()
    {
        float stat = initialDamage;
        for (int i = 1; i < shop.damageLevel; i++)
        {
            stat += Mathf.Round(stat * damageScale * 10) / 10;
        }
        damage = stat;
    }

    public void CritDamageCalculation()
    {
        float stat = initialCritDamage;
        for (int i = 1; i < shop.critDamageLevel; i++)
        {
            stat += critDamageScale;
        }
        critDamage = stat;
    }

    public void CritRateCalculation()
    {
        float stat = initialCritRate;
        for (int i = 1; i < shop.critRateLevel; i++)
        {
            stat += critRateScale;
        }
        critRate = stat;
    }

    public void AttackSpeedCalculation()
    {
        float stat = initialAttackSpeed;
        for (int i = 1; i < shop.attackSpeedLevel; i++)
        {
            stat -= attackSpeedScale;
        }
        attackSpeed = stat;
    }
    #endregion
}
