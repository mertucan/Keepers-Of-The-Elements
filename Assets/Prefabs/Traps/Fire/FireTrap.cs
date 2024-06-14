using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageInterval = 1.0f;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;
    private float damageTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                Damagable damagable = collision.GetComponent<Damagable>();
                if (damagable != null)
                {
                    damagable.Hit((int)damage, Vector2.zero); // Burada knockback verisini Vector2.zero olarak kullanıyoruz.
                }
                damageTimer = 0; // Zarar verme zamanlayıcısını sıfırla
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            damageTimer = 0;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;

        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
