using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
        set
        {
            animator.SetBool("lockVelocity", value);
        }
    }

    public UnityEvent<int, Vector2> damagableHit;
    public UnityEvent OnDeath; // Add OnDeath event
    Animator animator;

    [SerializeField] private float _maxHealth = 100;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField] private float _health = 100;
    [SerializeField] private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
            Debug.Log("isAlive set: " + value);
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
                OnDeath?.Invoke(); // Invoke the OnDeath event
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [SerializeField] private bool isInvincible;

    [SerializeField] private int armor = 0; // Set default armor to 0

    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            // Only apply armor reduction if the tag is "Player"
            if (gameObject.CompareTag("Player"))
            {
                damage -= armor;
                if (damage < 0)
                {
                    damage = 0;
                }
            }

            Health -= damage;
            isInvincible = true;
            animator.SetTrigger("hit");
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);
            return true;
        }

        return false;
    }

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }
}
