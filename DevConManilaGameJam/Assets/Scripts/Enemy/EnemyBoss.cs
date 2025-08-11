using UnityEngine;

public class EnemyBoss : EnemyRange
{
    public override void Start()
    {
        base.Start();
        attackDistance = 6;
    }
}
