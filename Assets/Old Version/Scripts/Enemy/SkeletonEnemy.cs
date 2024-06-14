using System;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        // Ana karakter ölmediyse ve saldırı aralığı dolmuşsa saldır
        if (PlayerInSight() && playerHealth.currentHealth > 0 && cooldownTimer >= attackCooldown)
        {
            anim.SetTrigger("meleeattack");
            cooldownTimer = 0;
            StopEnemyMovement();
        }

        // Saldırı aralığını güncelle
        cooldownTimer += Time.deltaTime;
    }

    private void StopEnemyMovement()
    {
        // Düşman karakterin hareketini durdurmak için buraya gerekli kodu yazın
        // Örneğin, düşmanın Rigidbody'sini etkisiz hale getirebiliriz
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            anim.SetTrigger("meleeattack");
            playerHealth.TakeDamage(damage);
        }
    }
}
