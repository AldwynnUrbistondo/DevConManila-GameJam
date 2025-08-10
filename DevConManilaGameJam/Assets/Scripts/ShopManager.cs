using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TextMeshProUGUI coinsText;
    public PlayerStats playerStats;
    public PetStats laserPetStats;
    public PetStats cryoPetStats;
    public PetStats energyWavePetStats;

    [Header("Buttons")]
    public Button healthButton;
    public Button healthRegenButton;
    public Button damageButton;
    public Button critDamageButton;
    public Button critRateButton;
    public Button attackSpeedButton;

    public Button laserPetButton;
    public Button cryoPetButton;
    public Button energyWavePetButton;

    [Header("TextMeshPro")]
    public TextMeshProUGUI healthPriceText;
    public TextMeshProUGUI healthRegenPriceText;
    public TextMeshProUGUI damagePriceText;
    public TextMeshProUGUI critDamagePriceText;
    public TextMeshProUGUI critRatePriceText;
    public TextMeshProUGUI attackSpeedPriceText;

    public TextMeshProUGUI laserPetPriceText;
    public TextMeshProUGUI cryoPetPriceText;
    public TextMeshProUGUI energyWavePetPriceText;

    [Header("Price")]
    public int healthPrice;
    public int healthRegenPrice;
    public int damagePrice;
    public int critDamagePrice;
    public int critRatePrice;
    public int attackSpeedPrice;

    public int laserPetPrice;
    public int cryoPetPrice;
    public int energyWavePetPrice;

    [Header("Price Scaling")]
    public float healthPriceScale;
    public float healthRegenPriceScale;
    public float damagePriceScale;
    public float critDamagePriceScale;
    public float critRatePriceScale;
    public float attackSpeedPriceScale;

    public float laserPetPriceScale;
    public float cryoPetPriceScale;
    public float energyWavePetPriceScale;

    [Header("Level")]
    public int healthLevel;
    public int healthRegenLevel;
    public int damageLevel;
    public int critDamageLevel;
    public int critRateLevel;
    public int attackSpeedLevel;

    public int laserPetLevel;
    public int cryoPetLevel;
    public int energyWavePetLevel;

    private void Start()
    {
        playerStats = FindAnyObjectByType<PlayerStats>();
        SetButtonFunctions();
        InitialValue();
        UpdatePrices(); // Calculate initial prices
        UpdateButtonState();
    }

    private void InitialValue()
    {
        // Levels
        healthLevel = 1;
        healthRegenLevel = 1;
        damageLevel = 1;
        critDamageLevel = 1;
        critRateLevel = 1;
        attackSpeedLevel = 1;

        laserPetLevel = 0;
        cryoPetLevel = 0;
        energyWavePetLevel = 0;
    }

    // Calculate all current upgrade prices
    private void UpdatePrices()
    {
        healthPrice = CalculatePrice(10, healthLevel - 1, healthPriceScale);
        healthRegenPrice = CalculatePrice(10, healthRegenLevel - 1, healthRegenPriceScale);
        damagePrice = CalculatePrice(10, damageLevel - 1, damagePriceScale);
        critDamagePrice = CalculatePrice(10, critDamageLevel - 1, critDamagePriceScale);
        critRatePrice = CalculatePrice(30, critRateLevel - 1, critRatePriceScale);
        attackSpeedPrice = CalculatePrice(30, attackSpeedLevel - 1, attackSpeedPriceScale);

        laserPetPrice = CalculatePrice(50, laserPetLevel, laserPetPriceScale);
        cryoPetPrice = CalculatePrice(30, cryoPetLevel, cryoPetPriceScale);
        energyWavePetPrice = CalculatePrice(100, energyWavePetLevel, energyWavePetPriceScale);
    }

    public void UpdateButtonState()
    {
        UpdatePrices(); // Always update prices first
        coinsText.text = $"Credits: {coins}";

        // Enable/Disable buttons
        healthButton.interactable = coins >= healthPrice;
        healthRegenButton.interactable = coins >= healthRegenPrice;
        damageButton.interactable = coins >= damagePrice;
        critDamageButton.interactable = coins >= critDamagePrice;

        critRateButton.interactable = coins >= critRatePrice && critRateLevel < 20;
        attackSpeedButton.interactable = coins >= attackSpeedPrice && attackSpeedLevel < 20;

        laserPetButton.interactable = coins >= laserPetPrice;
        cryoPetButton.interactable = coins >= cryoPetPrice;
        energyWavePetButton.interactable = coins >= energyWavePetPrice;

        // Price texts
        healthPriceText.text = healthPrice.ToString();
        healthRegenPriceText.text = healthRegenPrice.ToString();
        damagePriceText.text = damagePrice.ToString();
        critDamagePriceText.text = critDamagePrice.ToString();

        critRatePriceText.text = (critRateLevel < 20) ? critRatePrice.ToString() : "Max";
        attackSpeedPriceText.text = (attackSpeedLevel < 20) ? attackSpeedPrice.ToString() : "Max";

        laserPetPriceText.text = laserPetPrice.ToString();
        cryoPetPriceText.text = cryoPetPrice.ToString();
        energyWavePetPriceText.text = energyWavePetPrice.ToString();
    }

    public void SetButtonFunctions()
    {
        healthButton.onClick.AddListener(HealthUpgrade);
        healthRegenButton.onClick.AddListener(HealthRegenUpgrade);
        damageButton.onClick.AddListener(DamageUpgrade);
        critDamageButton.onClick.AddListener(CritDamageUpgrade);
        critRateButton.onClick.AddListener(CritRateUpgrade);
        attackSpeedButton.onClick.AddListener(AttackSpeedUpgrade);

        laserPetButton.onClick.AddListener(LaserPetUpgrade);
        cryoPetButton.onClick.AddListener(CryoPetUpgrade);
        energyWavePetButton.onClick.AddListener(EnergyWavePetUpgrade);
    }

    #region Upgrade Price Calculation
    // Shared price calculation
    private int CalculatePrice(int basePrice, int level, float increasePercent)
    {
        int price = basePrice;
        for (int i = 0; i < level; i++)
        {
            price += (int)(price * increasePercent);
        }
        //price = (int)(Math.Round(price / 5.0) * 5);
        return price;
    }

    public void HealthUpgrade()
    {
        if (coins >= healthPrice)
        {
            coins -= healthPrice;
            healthLevel++;
            UpdateButtonState();
            playerStats.UpdateStats();
        }
    }

    public void HealthRegenUpgrade()
    {
        if (coins >= healthRegenPrice)
        {
            coins -= healthRegenPrice;
            healthRegenLevel++;
            UpdateButtonState();
            playerStats.UpdateStats();
        }
    }

    public void DamageUpgrade()
    {
        if (coins >= damagePrice)
        {
            coins -= damagePrice;
            damageLevel++;
            UpdateButtonState();
            playerStats.UpdateStats();
        }
    }

    public void CritDamageUpgrade()
    {
        if (coins >= critDamagePrice)
        {
            coins -= critDamagePrice;
            critDamageLevel++;
            UpdateButtonState();
            playerStats.UpdateStats();
        }
    }

    public void CritRateUpgrade()
    {
        if (coins >= critRatePrice)
        {
            coins -= critRatePrice;
            critRateLevel++;
            UpdateButtonState();
            playerStats.UpdateStats();
        }
    }

    public void AttackSpeedUpgrade()
    {
        if (coins >= attackSpeedPrice)
        {
            coins -= attackSpeedPrice;
            attackSpeedLevel++;
            UpdateButtonState();
            playerStats.UpdateStats();
        }
    }

    public void LaserPetUpgrade()
    {
        if (coins >= laserPetPrice)
        {
            coins -= laserPetPrice;
            laserPetLevel++;
            UpdateButtonState();
            if (laserPetLevel > 1)
            {
                laserPetStats.UpdateStats();
            }
            else
            {
                laserPetStats.gameObject.SetActive(true);
            }
           
        }
    }

    public void CryoPetUpgrade()
    {
        if (coins >= cryoPetPrice)
        {
            coins -= cryoPetPrice;
            cryoPetLevel++;
            UpdateButtonState();
            if (cryoPetLevel > 1)
            {
                cryoPetStats.UpdateStats();
            }
            else
            {
                cryoPetStats.gameObject.SetActive(true);
            }
        }
    }

    public void EnergyWavePetUpgrade()
    {
        if (coins >= energyWavePetPrice)
        {
            coins -= energyWavePetPrice;
            energyWavePetLevel++;
            UpdateButtonState();
            if (energyWavePetLevel > 1)
            {
                energyWavePetStats.UpdateStats();
            }
            else
            {
                energyWavePetStats.gameObject.SetActive(true);
            }
        }
    }
    #endregion

    public int PetLevel(PetType type)
    {
        if (type == PetType.Laser)
        {
            return laserPetLevel;
        }
        else if (type == PetType.Cryo)
        {
            return cryoPetLevel;
        }
        else
        {
            return energyWavePetLevel;
        }
    }
}