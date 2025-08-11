using UnityEngine;

public class EnemyProjectile : Projectile
{
    //public Vector2 direction;
    public override void Shoot()
    {
        /*Vector2 direction = (target.position - transform.position).normalized;

        direction.y = 0;
        direction = direction.normalized;

        if (direction == Vector2.zero)
        {
            direction = Vector2.right;
        }
        */
        rb.linearVelocity = direction * 10;
    }

    public override void OnTriggerEnter2D(Collider2D actor)
    {

        PlayerStats playerStats = actor.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
