using UnityEngine;

public class ProjectileIce : Projectile
{
    public override void OnTriggerEnter2D(Collider2D actor)
    {
        IDamageable damageable = actor.GetComponent<IDamageable>();
        Enemy enemy = actor.GetComponent<Enemy>();
        if (damageable != null && enemy != null)
        {
            damageable.TakeDamage(damage);
            enemy.isFrozen = true;
            enemy.spriteRenderer.color = Color.blue;
            Destroy(gameObject);
        }

    }
}
