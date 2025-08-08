using UnityEngine;

public class EnemyRange : Enemy
{
    public float attackDistance;

    public override void Update()
    {
        if (!TargetInRange())
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

    }

    public bool TargetInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackDistance, LayerMask.GetMask("Player"));
        return hit;

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
