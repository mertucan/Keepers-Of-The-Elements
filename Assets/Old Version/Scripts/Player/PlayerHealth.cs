using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private PlayerController playerController;
    private SkeletonEnemy skeletonEnemy;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        skeletonEnemy = GetComponentInParent<SkeletonEnemy>();
    }

    public void PlayHurtAnimation()
    {
        anim.Play("KN_Hurt");
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            Debug.Log("Hit!");
            anim.SetTrigger("hit");
            playerController.PlayAnimation("Hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                Debug.Log("Dead!");
                Die();
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Die()
    {
        // Deactivate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = false;

        // Stop character movement
        playerController.enabled = false;

        // Disable rigidbody to prevent further movement
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        // Play death animation
        playerController.PlayAnimation("Die");

        // Restart scene after 3 seconds
        StartCoroutine(RestartSceneAfterDelay(3f));

        dead = true;
    }


    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}
