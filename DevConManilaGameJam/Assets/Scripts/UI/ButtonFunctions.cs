using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public AudioManager audioManager;
    public Animator saveAnim;
    public Button[] clickableButtons;
    public Button[] purchaseButtons;

    public Button pauseButton;
    public Button resumeButton;
    public Button saveButton;

    public Button quitButton;
    public string sceneName;

    private void Start()
    {
        if (audioManager != null)
        {
            foreach (Button b in clickableButtons)
            {
                b.onClick.AddListener(AddButtonClickSound);
            }

            foreach (Button b in purchaseButtons)
            {
                b.onClick.AddListener(AddBuySound);
            }
        }

        pauseButton.onClick.AddListener(PauseButton);

        resumeButton.onClick.AddListener(ResumeButton);

        saveButton.onClick.AddListener(ResumeButton);
        saveButton.onClick.AddListener(SaveButton);

        quitButton.onClick.AddListener(SaveButton);
        quitButton.onClick.AddListener(QuitButton);

    }

    void AddButtonClickSound()
    {
        audioManager.PlaySound(SoundType.ButtonClick);
    }

    void AddBuySound()
    {
        audioManager.PlaySound(SoundType.ButtonBuy);
    }

    void PauseButton()
    {
        GameManager.PauseGame();
    }

    void ResumeButton()
    {
        GameManager.UnPauseGame();
    }

    void SaveButton()
    {
        saveAnim.Play("Save");

        ShopManager sm = FindAnyObjectByType<ShopManager>();

        // Save currency
        PlayerPrefs.SetInt("Credits", sm.coins);

        // Save all upgrade levels
        PlayerPrefs.SetInt("Health Level", sm.healthLevel);
        PlayerPrefs.SetInt("Health Regen Level", sm.healthRegenLevel);
        PlayerPrefs.SetInt("Damage Level", sm.damageLevel);
        PlayerPrefs.SetInt("Crit Damage Level", sm.critDamageLevel);
        PlayerPrefs.SetInt("Crit Rate Level", sm.critRateLevel);
        PlayerPrefs.SetInt("Attack Speed Level", sm.attackSpeedLevel);

        // Save all pet levels
        PlayerPrefs.SetInt("Laser Pet Level", sm.laserPetLevel);
        PlayerPrefs.SetInt("Cryo Pet Level", sm.cryoPetLevel);
        PlayerPrefs.SetInt("Energy Wave Pet Level", sm.energyWavePetLevel);

        SpawnManager sp = FindAnyObjectByType<SpawnManager>();


        if (sp.wave < 11)
        {
            PlayerPrefs.SetInt("Checkpoint Wave", 1);
        }
        else
        {
            for (int i = 1; i <= sp.wave; i++)
            {
                if (i % 10 == 1)
                {
                    PlayerPrefs.SetInt("Checkpoint Wave", i);
                }
            }
        }
        
        // Actually write them to disk
        PlayerPrefs.Save();
    }

    void QuitButton()
    {
        SceneManager.LoadScene(sceneName);
    }

}
