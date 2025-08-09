using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float healthRegen;
    public float damage;
    public float critDamage;

    public float critRate;
    public float attackSpeed;

    public PlayerAttack playerAttack;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        UpdateStats();
    }

    public void UpdateStats()
    {
        playerAttack.fireRate = attackSpeed;
        playerAttack.damage = damage;
        playerAttack.critRate = critRate;
        playerAttack.critDamage = critDamage;
    }
}
