using UnityEngine;

public class EnemyBoss : EnemyRange
{
    public override void Start()
    {
        base.Start();
        attackDistance = 6;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.98f);
    }
}
