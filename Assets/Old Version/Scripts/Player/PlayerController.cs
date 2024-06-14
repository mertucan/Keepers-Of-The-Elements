using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string charName;
    private float speed = 16f, horizontal, jumpingPower = 22f, attackDuration = 0.5f;
    private float rollSpeed = 20f;
    private bool isFacingRight = true, isRolling = false;
    private float attackTimer = 0f, rollTimer = 0f;
    private int clickCount = 0;
    private float lastClickTime = 0f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int damage = 1;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isRolling)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2((horizontal * speed), jumpingPower);
                PlayAnimation("Jump");
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x * 1, rb.velocity.y * 0.5f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                if (timeSinceLastClick > attackDuration)
                {
                    clickCount = 0;
                }
                clickCount++;
                lastClickTime = Time.time;
                attackTimer = attackDuration;
                if (IsGrounded())
                {
                    rb.velocity = new Vector2(0f, rb.velocity.y); 
                }

                Attack();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded())
            {
                isRolling = true;
                rb.velocity = new Vector2((isFacingRight ? 1 : -1) * rollSpeed, 0f);
                rollTimer = 0.6f; // Roll animasyon süresi
                PlayAnimation("Roll");
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded() && Mathf.Abs(horizontal) < 0.1f)
            {
                isRolling = true;
                rb.velocity = new Vector2((isFacingRight ? 1 : -1) * speed, 0f);
                rollTimer = 0.6f; // Roll animasyon süresi
                PlayAnimation("Roll");
            }

            Flip();

            if (attackTimer > 0)
            {
                if (clickCount == 1)
                {
                    PlayAnimation("Attack");
                }
                else if (clickCount == 2)
                {
                    PlayAnimation("Second");
                } 
                else if (clickCount == 3)
                {
                    PlayAnimation("Third");
                    clickCount = 0;
                }

                attackTimer -= Time.deltaTime;
            }
            else if (Mathf.Abs(horizontal) > 0.1f && IsGrounded())
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                PlayAnimation("Walk");
            }
            else if (!IsGrounded())
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                PlayAnimation("Jump");
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                PlayAnimation("Idle");
            }
        }
        else
        {
            rb.velocity = new Vector2((isFacingRight ? 1 : -1) * rollSpeed, 0f);
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0)
            {
                isRolling = false;
            }
            if (Input.GetButtonDown("Jump")) // Zıplama tuşuna basıldığında rollu boz ve zıpla
            {
                isRolling = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                PlayAnimation("Jump");
            }
        }
    }

    private void FixedUpdate()
    {
        // FixedUpdate içindeki hareket kodları kaldırıldı
    }

    void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void PlayAnimation(string currentAction)
    {
        string animationName = $"{charName}_{currentAction}";
        animator.Play(animationName);
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}