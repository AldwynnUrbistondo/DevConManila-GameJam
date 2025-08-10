using UnityEngine;
using System.Collections;

public class EnemyRange : Enemy
{
    public float attackDistance;
    public GameObject projectilePrefab;

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

    public override IEnumerator AttackTarget()
    {
        //float animationRemainingTime = attackClip.length - attackLandingTime;
        // anim.Play("Attack");

        //yield return new WaitForSeconds(attackLandingTime);
        if (targetRay.collider != null)
        {
            GameObject prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            EnemyProjectile prjScript = prj.GetComponent<EnemyProjectile>();
            prjScript.damage = damage;
            if (isFacingRight)
            {
                prjScript.direction = Vector2.right;
            }
            else
            {
                prjScript.direction = -Vector2.right;
            }
        }
        //yield return new WaitForSeconds(animationRemainingTime);
        yield return new WaitForSeconds(2);

        isAttacking = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
