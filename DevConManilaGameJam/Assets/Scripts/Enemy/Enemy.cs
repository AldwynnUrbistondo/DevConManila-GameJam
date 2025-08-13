using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [HideInInspector] public Transform playerPos;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public RaycastHit2D targetRay;
    [HideInInspector] public AudioManager am;
    [HideInInspector] public Animator anim;

    [Header("Clips")]
    public AnimationClip attackClip;
    public AnimationClip deathClip;
    public float attackLandingTime;

    public float currentHealth;
    public float maxHealth;
    public float damage;
    public bool isDying = false;
    public bool isAttacking = false;

    public bool isFrozen = false;

    public float moveSpeed = 2;
    public int coinDrop;

    [HideInInspector] public bool isFacingRight = true;

    public Transform healthBarCanvas;

    public virtual void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        am = FindAnyObjectByType<AudioManager>();
        anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if (!TargetInRange() && !isAttacking && !isDying)
        {
            Vector2 direction = (playerPos.position - transform.position).normalized;
            if (!isFrozen)
            {
                rb.linearVelocity = new Vector2(direction.x * moveSpeed, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(direction.x * (moveSpeed / 2), 0);
            }

            Flip(direction);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (!isAttacking && TargetInRange() && !isDying)
        {
            isAttacking = true;
            StartCoroutine(AttackTarget());
        }

        if (rb.linearVelocity != Vector2.zero && !isDying)
        {
            anim.Play("Walk");
        }

    }

    #region TakeDamage and Die
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        am.PlaySound(SoundType.PlayerHitEnemy);
        StartCoroutine(HitColor());

        if (currentHealth <= 0)
        {
            Die();
        }

        
    }

    public void Die()
    {
        if (!isDying)
        {
            isDying = true;
            StartCoroutine(StartDie());
        }
        
    }

    public IEnumerator StartDie()
    {
        am.PlaySound(SoundType.EnemyDeath);
        Collider2D col = GetComponent<Collider2D>();
        ShopManager shop = FindAnyObjectByType<ShopManager>();
        shop.coins += coinDrop;
        shop.UpdateButtonState();

        if (col != null)
        {
            col.enabled = false;
        }
        anim.Play("Die");
        yield return new WaitForSeconds(deathClip.length);
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

    public virtual bool TargetInRange()
    {
        return false;
    }

    public virtual IEnumerator AttackTarget()
    {
        yield return null;
    }

    IEnumerator HitColor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        Color colorA;
        if (isFrozen)
        {
            //colorA = Color.blue; // new Color(0.68f, 0.85f, 0.9f); // LightBlue (RGB: 173, 216, 230)
            colorA = new Color(139f / 255f, 214f / 255f, 248f / 255f);
        }
        else
        {
            colorA = Color.white;
        }

        Color colorB = Color.red;
        float duration = 0.1f;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            sprite.color = Color.Lerp(colorA, colorB, t);
            yield return null;
        }

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            sprite.color = Color.Lerp(colorB, colorA, t);
            yield return null;
        }

    }
}
