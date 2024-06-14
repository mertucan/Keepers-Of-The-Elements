using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Düşmanın hareket hızı
    public float minWaitTime = 3f; // Minimum bekleme süresi
    public float maxWaitTime = 5f; // Maximum bekleme süresi
    public float detectionRadius = 5f; // Oyuncuyu algılama yarıçapı
    public LayerMask playerLayer; // Oyuncu layer'ı

    private bool isFollowingPlayer = false; // Oyuncuyu takip ediyor mu?

    private SpriteRenderer spriteRenderer; // SpriteRenderer bileşeni
    private CircleCollider2D detectionCollider; // Algılama yarıçapı collider'ı
    private Animator animator; // Animator bileşeni

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        detectionCollider = gameObject.AddComponent<CircleCollider2D>();
        detectionCollider.radius = detectionRadius;
        detectionCollider.isTrigger = true;
        animator = GetComponent<Animator>();
        StartCoroutine(RandomMovement());
        Physics2D.gravity = new Vector2(0f, -5.6f); // Yerçekimini ayarla
    }

    private void Update()
    {
        FindClosestPlayer();
        
        if (!isFollowingPlayer)
        {
            MoveRandomly();
        }
        else
        {
            FollowPlayer();
        }
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            if (!isFollowingPlayer)
            {
                float randomDirection = Random.Range(0f, 1f);
                if (randomDirection < 0.5f)
                {
                    // Sağa git
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    // Sola git
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
        }
    }
    

    private void MoveRandomly()
    {
        if (transform.localScale.x > 0)
        {
            // Sağa bakıyorsa
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Sola bakıyorsa
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        animator.SetBool("moving", true);
    }

    private void FollowPlayer()
    {
        GameObject player = FindClosestPlayer();
        if (player != null)
        {
            // Oyuncuyu takip et
            Vector2 direction = player.transform.position - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);

            // Yönü kontrol et ve sprite'ı döndür
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        animator.SetBool("moving", true);
    }

    private GameObject FindClosestPlayer()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        if (players.Length == 0)
        {
            isFollowingPlayer = false;
            return null;
        }

        isFollowingPlayer = true;
        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (Collider2D player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.gameObject;
            }
        }

        return closestPlayer;
    }

    private void OnDrawGizmos()
    {
        // Algılama yarıçapını görsel olarak çiz
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer düşman "Ground" layer'ına ait bir objeye temas ettiyse, aşağı düşmesini engelle
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
