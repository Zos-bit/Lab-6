using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowerEnemy : EnemyController
{
    public Transform target;

    protected override void Awake()
    {
        base.Awake();
        FindTargetIfNeeded();
    }

    protected override void FixedUpdate()
    {
        FindTargetIfNeeded();
        if (target == null)
        {
            return;
        }

        Vector2 nextPosition = Vector2.MoveTowards(
            body.position,
            target.position,
            maxSpeed * Time.fixedDeltaTime);
        body.MovePosition(nextPosition);
    }

    private void FindTargetIfNeeded()
    {
        if (target != null)
        {
            return;
        }

        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        if (player != null)
        {
            target = player.transform;
        }
    }

    protected override void HandleContact(GameObject other)
    {
        PlayerStats player = other.GetComponentInParent<PlayerStats>();
        if (player != null)
        {
            if (TryHandleContact(player))
            {
                player.TakeDamage(damage);
            }
            return;
        }

        if (IsWall(other) && TryHandleContact(other.transform.root))
        {
            HandleWallCollision();
        }
    }

    protected override void HandleWallCollision()
    {
        FlipDirection();
    }
}
