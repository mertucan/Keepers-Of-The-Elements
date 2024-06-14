using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] public float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Detection parameters")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask playerLayer;
    private bool isFollowingPlayer = false;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    public void OnDisable()
    {
        anim.SetBool("moving", false);
        isFollowingPlayer = false;
    }

    private void Update()
    {
        if (!isFollowingPlayer)
        {
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                    MoveInDirection(-1);
                else
                    DirectionChange();
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                    MoveInDirection(1);
                else
                    DirectionChange();
            }
        }
        else
        {
            FollowPlayer();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
            idleTimer = 0f;
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0f;
        anim.SetBool("moving", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in the direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void FollowPlayer()
    {
        // Check if the player is within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        if (colliders.Length > 0)
        {
            Vector3 playerPosition = colliders[0].transform.position;
            float direction = playerPosition.x > transform.position.x ? 1f : -1f;

            // Make the enemy face the player
            enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

            // Move towards the player
            enemy.position = Vector3.MoveTowards(enemy.position, playerPosition, speed * Time.deltaTime);
        }
        else
        {
            isFollowingPlayer = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
