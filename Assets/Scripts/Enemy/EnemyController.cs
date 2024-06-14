using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class EnemyController : MonoBehaviour
{
    public float walkSpeed = 30f;
    public float maxSpeed = 3f;
    Animator animator;
    TouchingDirections touchingDirections;
    Damagable damagable;
    Rigidbody2D rb;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public GameObject expOrbPrefab; // Prefab for the experience orb
    public GameObject healthPickupPrefab; // Prefab for the health pickup
    public float expValue = 10f; // Experience value of the enemy

    public enum WalkableDirection { Right, Left }
    public float walkStopRate = 0.05f;
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (_walkDirection == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (_walkDirection == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat("attackCooldown");
        }
        private set
        {
            animator.SetFloat("attackCooldown", Mathf.Max(value, 0));
        }
    }
    //
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damagable>();
        damagable.OnDeath.AddListener(DropExperienceOrbs); // Subscribe to the OnDeath event
    }

    private void OnDestroy()
    {
        damagable.OnDeath.RemoveListener(DropExperienceOrbs); // Unsubscribe from the OnDeath event
    }

    private void FixedUpdate()
    {
        if (touchingDirections.isOnWall && touchingDirections.isGrounded)
        {
            FlipDirection();
        }
        if (!damagable.LockVelocity)
        {
            if (CanMove && !HasTarget)  // Hedef varsa hareket etmeyi bırak
            {
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkSpeed * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }

        if (HasTarget && AttackCooldown <= 0)
        {
            Attack();  // Saldırı yap
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.Log("Current location not set to right or left.");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }

    private void Attack()
    {
        // Saldırı animasyonu tetiklenir
        animator.SetTrigger("attack");
        // Saldırı yapıldıktan sonra cooldown başlatılır
        AttackCooldown = animator.GetFloat("attackCooldownDuration");
    }

    private void DropExperienceOrbs()
    {
        int orbCount = Random.Range(3, 5); // Rastgele 3 veya 4 ExpOrb düşür
        float scatterRadius = 2.0f; // Increase this value to scatter orbs further

        for (int i = 0; i < orbCount; i++)
        {
            Vector2 dropPosition = new Vector2(
                transform.position.x + Random.Range(-scatterRadius, scatterRadius), 
                transform.position.y + Random.Range(-scatterRadius, scatterRadius)
            );
            Instantiate(expOrbPrefab, dropPosition, Quaternion.identity);
        }

        // 25% chance to drop a health pickup
        if (Random.value <= 0.25f)
        {
            Vector2 healthDropPosition = new Vector2(
                transform.position.x + Random.Range(-scatterRadius, scatterRadius), 
                transform.position.y + Random.Range(-scatterRadius, scatterRadius)
            );
            Instantiate(healthPickupPrefab, healthDropPosition, Quaternion.identity);
        }
    }
}
