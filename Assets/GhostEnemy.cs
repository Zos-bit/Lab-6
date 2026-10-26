using UnityEngine;

public class GhostEnemy : EnemyController
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsWall(collision.gameObject))
        {
            foreach (Collider2D ownCollider in GetComponentsInChildren<Collider2D>())
            {
                Physics2D.IgnoreCollision(ownCollider, collision.collider);
            }
            return;
        }

        base.OnCollisionEnter2D(collision);
    }

    protected override void HandleWallCollision()
    {
        // Ghosts continue moving through walls, including walls configured as triggers.
    }
}
