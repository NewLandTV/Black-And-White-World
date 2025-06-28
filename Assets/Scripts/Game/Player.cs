using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3.5f;
    [SerializeField]
    private KeyCode[] moveLeftKey = new KeyCode[] { KeyCode.A, KeyCode.LeftArrow };
    [SerializeField]
    private KeyCode[] moveRightKey = new KeyCode[] { KeyCode.D, KeyCode.RightArrow };

    private float velocityX;
    private bool walking;

    [SerializeField]
    private float jumpForce = 2f;
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    private LayerMask groundLayerMask = ~0;
    [SerializeField]
    private float checkGroundDistance = 1.2f;

    private bool jumpKeyPressed;
    private bool jumping;
    private bool canJump;

    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    private Animator animator;

    private bool die;

    private void Awake()
    {
        SetupComponent();
    }

    private void Update()
    {
        if (GameManager.getClear() || die)
        {
            return;
        }

        InputMove();
        InputJump();
    }

    private void FixedUpdate()
    {
        if (GameManager.getClear() || die)
        {
            return;
        }

        Move();
        Jump();
        CheckGround();
    }

    private void SetupComponent()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void InputMove()
    {
        walking = true;

        for (int i = 0; i < moveLeftKey.Length; i++)
        {
            bool press = Input.GetKey(moveLeftKey[i]);

            if (press)
            {
                spriteRenderer.flipX = true;
                velocityX = -walkSpeed;

                animator.SetBool("Walking", walking);

                return;
            }
        }

        for (int i = 0; i < moveRightKey.Length; i++)
        {
            bool press = Input.GetKey(moveRightKey[i]);

            if (press)
            {
                spriteRenderer.flipX = false;
                velocityX = walkSpeed;

                animator.SetBool("Walking", walking);

                return;
            }
        }

        velocityX = 0f;
        walking = false;

        animator.SetBool("Walking", walking);
    }

    private void InputJump()
    {
        jumpKeyPressed = Input.GetKey(jumpKey);
        canJump = jumpKeyPressed && !jumping;
    }

    private void Move()
    {
        float y = rigidbody.velocity.y;
        Vector2 velocity = new Vector2(velocityX, y);

        rigidbody.velocity = velocity;
    }

    private void Jump()
    {
        if (!canJump)
        {
            return;
        }

        jumping = true;

        Vector2 force = Vector2.up * jumpForce;

        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, checkGroundDistance, groundLayerMask);

        if (hit.collider != null)
        {
            jumping = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("DIe Block") || die)
        {
            return;
        }

        die = true;

        GameManager.dieAction();
    }
}
