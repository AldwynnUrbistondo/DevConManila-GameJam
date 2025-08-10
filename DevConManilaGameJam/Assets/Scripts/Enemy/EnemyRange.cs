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

    public override void Update()
    {
        base.Update();

        if (isFacingRight)
        {
            Debug.DrawRay(transform.position, transform.right * attackDistance, Color.white);

        }
        else
        {
            Debug.DrawRay(transform.position, -transform.right * attackDistance, Color.white);
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
            prjScript.Shoot();
        }
        //yield return new WaitForSeconds(animationRemainingTime);
        yield return new WaitForSeconds(2);

        isAttacking = false;
    }

}
