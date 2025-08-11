using UnityEngine;

public class PlayerAttack : ShooterScript
{
    public Transform mouseTransform;
    public PlayerMovement playerMovement;

    public override void Start()
    {
        base.Start();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public override void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mouseTransform.position = mousePos;


        if (!GameManager.isPaused && GameManager.canShoot) 
        {
            fireInterval += Time.deltaTime;
            if (fireInterval >= fireRate && Input.GetMouseButton(0))
            {
                ShootEnemy(damage);
                fireInterval = 0;
                GameManager.canMove = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                GameManager.canMove = true;
            }
            
        }

    }

    public override void ShootEnemy(float damage)
    {
        float finalDamage = CritCalculation(damage);

        GameObject prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile prjScript = prj.GetComponent<Projectile>();
        prjScript.target = mouseTransform;
        prjScript.damage = finalDamage;
        prjScript.Shoot();

        //AudioManager audioManager = FindObjectOfType<AudioManager>();
        //audioManager.PlaySound(SoundType.PlayerProjectile);
        //}

    }
}
