using UnityEngine;

public class ProjectileIce : Projectile
{
    public override void OnTriggerEnter2D(Collider2D actor)
    {
        IDamageable damageable = actor.GetComponent<IDamageable>();
        Enemy enemy = actor.GetComponent<Enemy>();
        if (damageable != null && enemy != null)
        {

            enemy.isFrozen = true;
            damageable.TakeDamage(damage);
            Destroy(gameObject);


            /*
            if (!enemy.isDying)
            {
               
                enemy.spriteRenderer.color = new Color(139f / 255f, 214f / 255f, 248f / 255f);
            }
           */
           
        }

    }
}
