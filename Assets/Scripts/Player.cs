using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravityScaleSmall = 1.0f;
    [SerializeField] private float gravityScaleBig = 5.0f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX;
    private float lastVelocityPosY;
    private float lastVelocityNegY;
    private bool isSmall = true;
    private enum AnimationState { idle, running, jumping, falling };
    public LayerMask jumpableGround;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = rb.GetComponent<BoxCollider2D>();
        anim = rb.GetComponent<Animator>();
        sprite = rb.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Find out if the player went high enough in SMALL form to break a platform.
        if (rb.velocity.y >= 8.0f && isSmall) 
        {
            lastVelocityPosY = rb.velocity.y;
        } 

        if (rb.velocity.y <= -10.0f && !isSmall)
        {
            lastVelocityNegY = rb.velocity.y;
        }

        ChangePlayerSize();
        PlayerAnimationState();

    }

    private void ChangePlayerSize()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (transform.localScale == Vector3.one) // Checks if the player is small, then makes them big.
            {
                transform.localScale = Vector3.one + (Vector3.one / 2);
                rb.gravityScale = gravityScaleBig;
                isSmall = false;
            }
            else if (transform.localScale == Vector3.one * 1.5f) // Checks if the player is big, then makes them small.
            {
                transform.localScale = Vector3.one;
                rb.gravityScale = gravityScaleSmall;
                isSmall = true;
            }

        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void PlayerAnimationState()
    {
        AnimationState state;

        if (dirX >= 0.1f)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        else if (dirX <= -0.1f)
        {
            state = AnimationState.running;
            sprite.flipX = true;
        }
        else
        {
            state = AnimationState.idle;
        }
        anim.SetInteger("animation_state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the player turned into BIG while in mid air to break a platform
        if (collision.gameObject.CompareTag("BreakablePlatform")) 
        {
            if ((!isSmall && lastVelocityPosY >= 8.0f) || (!isSmall && lastVelocityNegY <= -10.0f))
            {
                Destroy(collision.gameObject);
            }
        }

        // Checks if you can force something to fall down while big with enough height.
        // This makes the object fall down
        if (collision.gameObject.CompareTag("Slamable") && IsGrounded()) 
        {
            if ((!isSmall && lastVelocityPosY >= 8.0f) || (!isSmall && lastVelocityNegY <= -10.0f))
            {
                Rigidbody2D collRB;
                collRB = collision.gameObject.GetComponent<Rigidbody2D>();
                collRB.bodyType = RigidbodyType2D.Dynamic;
                /*collRB.velocity = new Vector2(0.0f, collRB.velocity.y);
                collRB.angularVelocity = 0.0f; */
                
            }
        }



        // Check if the player touched a ground to reset the jump and reset last +Y and -Y velocity
        if (IsGrounded())
        {
            lastVelocityPosY = 0.0f;
            lastVelocityNegY = 0.0f;
        }


    }

}
