using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthValue = 20; // Health value of the pickup

    public float attractionRadius = 3.0f; // The radius within which the pickup will be attracted to the player
    public float attractionSpeed = 5.0f; // The speed at which the pickup will move towards the player
    public float floatSpeed = 1.0f; // Speed of the floating effect
    public float floatAmplitude = 0.5f; // Amplitude of the floating effect

    private GameObject player;
    private bool isAttracted = false;
    private Vector3 initialPosition;
    private float floatTimer = 0;

    void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check the distance to the player
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= attractionRadius)
            {
                isAttracted = true;
            }

            if (isAttracted)
            {
                // Move the pickup towards the player
                Vector2 direction = (player.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, attractionSpeed * Time.deltaTime);
            }
            else
            {
                // Floating effect when not attracted
                floatTimer += Time.deltaTime;
                transform.position = initialPosition + new Vector3(0, Mathf.Sin(floatTimer * floatSpeed) * floatAmplitude, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the pickup collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add health to the player
            Damagable playerDamagable = collision.gameObject.GetComponent<Damagable>();
            if (playerDamagable != null)
            {
                playerDamagable.Health += healthValue;
                if (playerDamagable.Health > playerDamagable.MaxHealth)
                {
                    playerDamagable.Health = playerDamagable.MaxHealth;
                }
            }

            // Destroy the pickup
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the collision is with the ground or another object
        // Move the pickup towards the player
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, attractionSpeed * Time.deltaTime);
        }
    }
}
