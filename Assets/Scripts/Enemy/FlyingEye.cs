using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damagable))]
public class FlyingEye : MonoBehaviour
{
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;
    public float waypointReachedDistance = 0.1f;
    public float flightSpeed = 3f;
    public GameObject expOrbPrefab; // Prefab for the experience orb
    public GameObject healthPickupPrefab; // Prefab for the health pickup
    public float expValue = 10f; // Experience value of the enemy

    private Rigidbody2D rb;
    private Animator animator;
    private Damagable damagable;
    private Transform nextWaypoint;
    private int waypointNum = 0;
    private bool _hasTarget = false;
    
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damagable = GetComponent<Damagable>();
        damagable.OnDeath.AddListener(DropExperienceOrbs); // Subscribe to the OnDeath event
    }

    private void OnDestroy()
    {
        damagable.OnDeath.RemoveListener(DropExperienceOrbs); // Unsubscribe from the OnDeath event
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damagable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.gravityScale = 0.8f;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void DropExperienceOrbs()
    {
        int orbCount = Random.Range(3, 5); // Randomly drop 3 or 4 ExpOrbs
        float scatterRadius = 3.0f; // Increase this value to scatter orbs further

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
