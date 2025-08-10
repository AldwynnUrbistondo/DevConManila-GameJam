using TMPro;
using UnityEngine;

public enum PetType
{
    Laser,
    Cryo,
    EnergyWave
}

public class PetStats : ShooterScript
{
    public ShopManager shop;
    public PetType type;

    [Header("Initial Stats")]
    public float petInitialDamage;
    public float petInitialCritDamage;

    public float petInitialCritRate;
    public float petInitialAttackSpeed;

    [Header("Max Stats")]

    public float petMaxCritRate;
    public float petMaxAttackSpeed;

    [Header("Stats")]
    public float petDamage;
    public float petCritDamage;

    public float petCritRate;
    public float petAttackSpeed;

    [Header("Scale")]
    public float petDamageScale;
    public float petCritDamageScale;

    public float petCritRateScale;
    public float petAttackSpeedScale;

    public TextMeshProUGUI levelText;

    private void Awake()
    {
        InitialValue();
        UpdateStats();
    }


    private void InitialValue()
    {
        petDamage = petInitialDamage;
        petCritDamage = petInitialCritDamage;
        petCritRate = petInitialCritRate;
        petAttackSpeed = petInitialAttackSpeed;

    }

    public void UpdateStats()
    {
        DamageCalculation();
        CritDamageCalculation();
        CritRateCalculation();
        AttackSpeedCalculation();

        damage = petDamage;
        critDamage = petCritDamage;
        critRate = petCritRate;
        fireRate = petAttackSpeed;

        levelText.text = $"Level: {shop.PetLevel(type)}";
    }

    public void DamageCalculation()
    {
        float stat = petInitialDamage;
        for (int i = 1; i < shop.PetLevel(type); i++)
        {
            stat += Mathf.Round(stat * petDamageScale * 10) / 10;
        }
        petDamage = stat;
    }

    public void CritDamageCalculation()
    {
        float stat = petInitialCritDamage;
        for (int i = 1; i < shop.PetLevel(type); i++)
        {
            stat += petCritDamageScale;
        }
        petCritDamage = stat;
    }

    public void CritRateCalculation()
    {
        float stat = petInitialCritRate;
        for (int i = 1; i < shop.PetLevel(type); i++)
        {
            stat += petCritRateScale;
        }
        stat = Mathf.Clamp(stat, 0, petMaxCritRate);
        petCritRate = stat;
    }

    public void AttackSpeedCalculation()
    {
        float stat = petInitialAttackSpeed;
        for (int i = 1; i < shop.PetLevel(type); i++)
        {
            stat -= petAttackSpeedScale;
        }
        stat = Mathf.Clamp(stat, petMaxAttackSpeed, petInitialAttackSpeed);
        petAttackSpeed = stat;
    }
}
