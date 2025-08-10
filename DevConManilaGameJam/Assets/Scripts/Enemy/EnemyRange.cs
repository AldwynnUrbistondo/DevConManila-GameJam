using UnityEngine;

public class EnemyRange : Enemy
{
    public float attackDistance;

    public override void Start()
    {
        base.Start();
        attackDistance = Random.Range(3f, 5f);
    }

    public override bool TargetInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackDistance, LayerMask.GetMask("Player"));
        return hit;

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
