using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class KnightController : MonoBehaviour
{
    public float walkSpeed = 16f;
    Rigidbody2D rb;
    Animator animator;
    Damagable damagable;
    AudioSource audioSource;
    public AudioClip swordHit;
    public bool CanMove {get {
        return animator.GetBool("canMove");
    }}
    public bool isAlive{
        get{
            return animator.GetBool("isAlive");
        }
    }
    [SerializeField] private bool _isMoving = false;
    public bool IsMoving{get{
        return _isMoving;
    }
    private set{
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    public bool _isFacingRight = true;
    public float jumpImpulse = 150f;
    public float CurrentMoveSpeed{ get{
        if(CanMove)
        {
            if(isMoving && !touchingDirections.isOnWall)
            {
                return walkSpeed;
            }

            else 
            {
                return 0;
            }
        }

        else
        {
            return 0;
        }
    }}
    public bool isFacingRight{get {
        return _isFacingRight;
    } 
    private set{
        if(_isFacingRight!=value)
        {
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;
    }}

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    public bool isMoving {get {
        return _isMoving;
    }
    private set{
        _isMoving = value;
        animator.SetBool("isMoving", value);
    }}

    //
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(isAlive)
        {
            isMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            isMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !isFacingRight)
        {
            //Face right
            isFacingRight = true;
        }
        else if(moveInput.x < 0 && isFacingRight)
        {
            //Face left
            isFacingRight = false;
        }
    }

    private void FixedUpdate()
    {
        if(!damagable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damagable = GetComponent<Damagable>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.isGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger("attack");
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        GetHitSound();
    }

    void GetHitSound()
    {
        audioSource.Play();
    }
    
    public void SwordSound()
    {
        audioSource.PlayOneShot(swordHit);
    }
}