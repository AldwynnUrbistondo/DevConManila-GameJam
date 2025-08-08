using UnityEngine;

public class EnemyMelee : Enemy
{
    RaycastHit2D targetRay;
    public float attackDistance;

    public override void Update()
    {
        if (!TargetInRange())
        {
            Vector2 direction = (playerPos.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * moveSpeed, 0);

            Flip(direction);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (isFacingRight)
        {
            Debug.DrawRay(transform.position, transform.right * attackDistance, Color.red);

        }
        else
        {
            Debug.DrawRay(transform.position, -transform.right * attackDistance, Color.red);
        }
    }

    public bool TargetInRange()
    {
        if (isFacingRight)
        {
            targetRay = Physics2D.Raycast(transform.position, transform.right, attackDistance, LayerMask.GetMask("Player"));
        }
        else
        {
            targetRay = Physics2D.Raycast(transform.position, -transform.right, attackDistance, LayerMask.GetMask("Player"));
        }

        if (targetRay.collider != null)
        {
            return true;
        }

        return false;
    }
}
