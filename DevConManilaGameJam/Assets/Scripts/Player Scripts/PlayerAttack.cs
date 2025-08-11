using System.Collections;
using UnityEngine;

public class PlayerAttack : ShooterScript
{
    public Transform mouseTransform;
    public PlayerMovement playerMovement;
    public Animator anim;
    public AnimationClip attackClip;
    bool isAttacking = false;

    public override void Start()
    {
        base.Start();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mouseTransform.position = mousePos;

        if (!GameManager.isPaused && GameManager.canShoot && !isAttacking)
        {
            if (Input.GetMouseButton(0))
            {
                ShootEnemy(damage);
            }
        }
    }

    public override void ShootEnemy(float damage)
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        // Speed up animation so it finishes within fireRate
        anim.speed = attackClip.length / fireRate;
        anim.Play("Attack");
        GameManager.canMove = false;

        // Wait for animation to finish (faster speed = shorter time)
        yield return new WaitForSeconds(fireRate);

        // Fire projectile right after animation
        float finalDamage = CritCalculation(damage);
        GameObject prj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile prjScript = prj.GetComponent<Projectile>();
        prjScript.direction = playerMovement.isFacingRight ? Vector2.right : -Vector2.right;
        prjScript.damage = finalDamage;
        prjScript.Shoot();

        anim.speed = 1;
        GameManager.canMove = true;

        // Cooldown is already handled by fireRate wait
        isAttacking = false;
    }

}
