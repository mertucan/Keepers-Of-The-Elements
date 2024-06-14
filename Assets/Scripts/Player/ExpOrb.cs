using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expValue = 20; // Experience value of the orb

    public float attractionRadius = 3.0f; // The radius within which the orb will be attracted to the player
    public float attractionSpeed = 5.0f; // The speed at which the orb will move towards the player
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
                // Move the orb towards the player
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
        // Check if the orb collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add experience to the player
            PlayerExperience playerExp = collision.gameObject.GetComponent<PlayerExperience>();
            if (playerExp != null)
            {
                playerExp.AddExperience(expValue);
            }

            // Destroy the orb
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the collision is with the ground or another object
        // Move the orb towards the player
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, attractionSpeed * Time.deltaTime);
        }
    }
}
