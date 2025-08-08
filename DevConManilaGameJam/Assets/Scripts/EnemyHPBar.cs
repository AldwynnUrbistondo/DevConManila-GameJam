using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public GameObject healthBarPanel;

    public float _health;
    public float _maxHealth;

    public float lerpTimer;
    public float chipSpeed = 2f;

    public Image frontHealthBar;
    public Image backHealthBar;

    IDamageable damageable;
    Enemy enemy;

    public virtual void Start()
    {
        damageable = GetComponentInParent<IDamageable>();
        enemy = GetComponentInParent<Enemy>();

        _maxHealth = enemy.maxHealth;
        _health = _maxHealth;

        healthBarPanel.SetActive(false);
    }

    public virtual void Update()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _health = enemy.currentHealth;

        // Show health bar when taken damage for the first time
        if (_health < _maxHealth && !healthBarPanel.activeSelf)
        {
            healthBarPanel.SetActive(true);
        }



        // Hide health bar when dead
        if (_health <= 0 || enemy.isDying)
        {
            healthBarPanel.SetActive(false);
        }

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = _health / _maxHealth;

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
