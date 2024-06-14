using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameObject player;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    public bool applyFireEffect = false;
    public bool applyWaterEffect = false;
    public GameObject waterBuffPrefab; // Prefab for Water Buff effect
    public Transform waterBuffSpawnPoint; // Transform to spawn Water Buff effect
    public int burnDamage = 5, waterBuffHeal = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damagable.Hit(attackDamage, knockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
                if (applyFireEffect)
                {
                    Debug.Log("Applying burn effect");
                    StartCoroutine(ApplyBurnEffect(damagable, burnDamage, 3));
                }

                if (applyWaterEffect && waterBuffPrefab != null && waterBuffSpawnPoint != null)
                {
                    InstantiateWaterBuff(waterBuffSpawnPoint.position);
                }
            }
        }
    }

    private IEnumerator ApplyBurnEffect(Damagable damagable, int burnDamagePerSecond, int duration)
    {
        int elapsed = 0;
        while (elapsed < duration)
        {
            Debug.Log("Burn effect applied: " + burnDamagePerSecond + " damage");
            damagable.Hit(burnDamagePerSecond, Vector2.zero);
            yield return new WaitForSeconds(1f);
            elapsed++;
        }
        Debug.Log("Burn effect ended");
    }

    private void InstantiateWaterBuff(Vector2 position)
    {
        Damagable playerDamagable = player.GetComponent<Damagable>();
        GameObject waterBuff = Instantiate(waterBuffPrefab, position, Quaternion.identity);
        playerDamagable.Health += waterBuffHeal;
        if (playerDamagable.Health > playerDamagable.MaxHealth)
        {
            playerDamagable.Health = playerDamagable.MaxHealth;
        }
        Destroy(waterBuff, 0.35f);
    }
}
