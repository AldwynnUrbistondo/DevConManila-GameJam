using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int coins;

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
        InitialValue();
    }

    private void InitialValue()
    {
        // = = = = = Price = = = = =
        healthPrice = 10;
        healthRegenPrice = 10;
        damagePrice = 10;
        critDamagePrice = 10;

        critRatePrice = 30;
        attackSpeedPrice = 30;

        laserPetPrice = 75;
        cryoPetPrice = 50;
        energyWavePetPrice = 150;

        // = = = = = Level = = = = = 
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

    public void UpdateButtonState()
    {

    }

}
