using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    [Min(0f)] public float maxSpeed = 2f;
    [Min(0)] public int damage = 1;

    protected Rigidbody2D body;
    protected SpriteRenderer spriteRenderer;
    protected float moveDirection = 1f;

    private readonly HashSet<Object> handledContacts = new HashSet<Object>();
    private int handledContactFrame = -1;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.flipX)
        {
            moveDirection = -1f;
        }
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        HandleContact(collision.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        HandleContact(other.gameObject);
    }

    protected virtual void HandleContact(GameObject other)
    {
        PlayerStats player = other.GetComponentInParent<PlayerStats>();
        if (player != null)
        {
            if (!TryHandleContact(player))
            {
                return;
            }
            player.TakeDamage(damage);
            FlipDirection();
            return;
        }

        if (IsWall(other))
        {
            if (!TryHandleContact(other.transform.root))
            {
                return;
            }
            HandleWallCollision();
        }
    }

    protected bool TryHandleContact(Object target)
    {
        if (handledContactFrame != Time.frameCount)
        {
            handledContacts.Clear();
            handledContactFrame = Time.frameCount;
        }
        return handledContacts.Add(target);
    }

    protected bool IsWall(GameObject other)
    {
        return other.CompareTag("Wall") || other.transform.root.CompareTag("Wall");
    }

    protected virtual void HandleWallCollision()
    {
        FlipDirection();
    }

    protected void FlipDirection()
    {
        moveDirection = -moveDirection;
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}
