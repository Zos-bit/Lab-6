using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkingEnemy : EnemyController
{
    protected override void FixedUpdate()
    {
        if (body != null)
        {
            body.linearVelocity = new Vector2(moveDirection * maxSpeed, body.linearVelocity.y);
        }
    }
}
