using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageCooldown = 1.5f;

    private Dictionary<Collider2D, float> lastDamageTime = new Dictionary<Collider2D, float>();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            Damagable damagable = collision.GetComponent<Damagable>();
            if (damagable != null)
            {
                if (!lastDamageTime.ContainsKey(collision))
                {
                    lastDamageTime[collision] = Time.time - damageCooldown;
                }
                
                AttemptDamage(collision, damagable);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Damagable damagable = collision.GetComponent<Damagable>();
            if (damagable != null)
            {
                AttemptDamage(collision, damagable);
            }
        }
    }

    private void AttemptDamage(Collider2D collision, Damagable damagable)
    {
        if (Time.time - lastDamageTime[collision] >= damageCooldown)
        {
            damagable.Hit((int)damage, Vector2.zero);
            lastDamageTime[collision] = Time.time;
            Debug.Log("Damage taken: " + damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (lastDamageTime.ContainsKey(collision))
            {
                lastDamageTime.Remove(collision);
            }
        }
    }
}
