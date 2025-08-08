using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float currentHealth;
    public float maxHealth;

    public bool isDying;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(StartDie());
    }

    public IEnumerator StartDie()
    {
        Collider2D col = GetComponent<Collider2D>();
        if(col != null)
        {
            col.enabled = false;
        }
        yield return null;
        Destroy(this.gameObject);
    }
}
