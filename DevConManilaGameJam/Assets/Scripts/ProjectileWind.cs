using UnityEngine;

public class ProjectileWind : Projectile
{
    public override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 0.5f);
    }
    public override void Shoot()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        direction.y = 0;
        direction = direction.normalized; 

        if (direction == Vector2.zero)
        {
            direction = Vector2.right;
        }

        rb.linearVelocity = direction * 10;
    }

    public override void OnTriggerEnter2D(Collider2D actor)
    {
        IDamageable damageable = actor.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

    }
}
