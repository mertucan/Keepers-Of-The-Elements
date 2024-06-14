using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private EnemyMovement enemyMovement;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hit");
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("isDead", true);

        // SpriteRenderer'ı 3 saniye sonra yok et
        StartCoroutine(DestroySpriteRendererAfterDelay(1.7f));
    }

    IEnumerator DestroySpriteRendererAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // SpriteRenderer'ı yok et
        Destroy(spriteRenderer.gameObject);
    }
}
