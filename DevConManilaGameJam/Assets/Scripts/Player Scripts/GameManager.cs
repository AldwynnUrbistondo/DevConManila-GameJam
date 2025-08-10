using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject healthBarPanel;

    public float lerpTimer;
    public float chipSpeed = 2f;

    public Image frontHealthBar;
    public Image backHealthBar;

    public virtual void Update()
    {

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = playerStats.currentHealth / playerStats.maxHealth;

        // When taking damage
        if (fillB > hFraction)
        {
            lerpTimer = 0f;
            frontHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        // When healing
        else if (fillF < hFraction)
        {
            lerpTimer = 0f;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }
}
