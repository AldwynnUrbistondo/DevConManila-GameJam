using UnityEngine;
using System.Collections;

public class EnemyMelee : Enemy
{
    public float attackDistance;

    public override void Start()
    {
        base.Start();
        attackDistance = Random.Range(1f, 2f);
    }

    public override void Update()
    {
        base.Update();

        if (isFacingRight)
        {
            Debug.DrawRay(transform.position, transform.right * attackDistance, Color.red);

        }
        else
        {
            Debug.DrawRay(transform.position, -transform.right * attackDistance, Color.red);
        }
    }

    public override bool TargetInRange()
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

    public override IEnumerator AttackTarget()
    {
        //float animationRemainingTime = attackClip.length - attackLandingTime;
        // anim.Play("Attack");

        //yield return new WaitForSeconds(attackLandingTime);

        if (targetRay.collider != null)
        {

            PlayerStats playerStats = targetRay.collider.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);

            }
        }
        //yield return new WaitForSeconds(animationRemainingTime);
        yield return new WaitForSeconds(2);

        isAttacking = false;
    }
}
