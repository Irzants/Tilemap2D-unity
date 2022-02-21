using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;


    Vector2 moveInput;
    Rigidbody2D rb;

    Animator myAnimator;
    CapsuleCollider2D collide;
    BoxCollider2D myFeet;

    float gravityScaleAtStart;

    bool isAlive = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        collide = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }


    void Update()
    {
       if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }


    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }


    void OnMove(InputValue value)
    {
        if(!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }


    void OnJump(InputValue value)
    {
        if(!isAlive) { return; }
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpHeight);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            rb.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimb", false);

            return;
        }

        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        rb.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimb", playerHasVerticalSpeed);


    }

    void Die()
    {
        if(collide.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("IsDead");
            rb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
        }
    }

}
