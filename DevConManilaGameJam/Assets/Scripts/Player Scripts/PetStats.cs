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
    public bool isFacingRight = true;
    public float movementSpeed = 3.5f;
    public Vector3 offSetToPlayer;
    public Transform playerPos;

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
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    public override void Update()
    {
        base.Update();
        GoToPlayer();
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

    public override void ShootEnemy(float damage)
    {
        if (nearestEnemy != null && GameManager.canPetShoot)
        {

            Vector2 directionToTarget = (nearestEnemy.position - transform.position).normalized;
            Flip(directionToTarget);

            float finalDamage = CritCalculation(damage);
            GameObject prj;

            if (firePoint != null)
            {
                prj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            }
            else
            {
                prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }

            Projectile prjScript = prj.GetComponent<Projectile>();
            prjScript.target = nearestEnemy;
            prjScript.damage = finalDamage;
            prjScript.Shoot();


            am.PlaySound(soundType);
        }

    }

    public void Flip(Vector2 direction)
    {
        if ((isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void GoToPlayer()
    {

        Vector2 targetPos = playerPos.position + offSetToPlayer;
        float distance = Vector2.Distance(transform.position, targetPos);

        if (distance > 0.05f)
        {
            Vector2 direction = ((playerPos.position + offSetToPlayer) - transform.position).normalized;
            rb.linearVelocity = direction * movementSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
