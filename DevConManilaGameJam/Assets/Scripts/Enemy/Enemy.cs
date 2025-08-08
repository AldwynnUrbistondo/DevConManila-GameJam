using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [HideInInspector] public Transform playerPos;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    public float currentHealth;
    public float maxHealth;
    public bool isDying;

    public bool isFrozen = false;

    public float moveSpeed = 2;
    [HideInInspector] public bool isFacingRight = true;

    public Transform healthBarCanvas;

    public void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        Vector2 direction = (playerPos.position - transform.position).normalized;
        if (!isFrozen)
        {
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(direction.x * (moveSpeed/2), 0);
        }

        Flip(direction);
    }

    #region TakeDamage and Die
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
    #endregion

    public void Flip(Vector2 direction)
    {
        if ((isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;


            if (healthBarCanvas != null)
            {
                Vector2 childScale = healthBarCanvas.localScale;
                childScale.x *= -1f;
                healthBarCanvas.localScale = childScale;
            }
        }
    }

}
