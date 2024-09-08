using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;

    private bool facingRight = true;
    private float move;
    private bool isAttacking = false;

    public float JumpForce;
    public bool IsJumping = false;
    public bool IsGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            CharacterMovement();
        }
        CharacterAttack();
        CharacterDashAttack();
        CharacterJump();
    }

    void CharacterMovement()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        CharacterAnimation(move);
    }

    void CharacterAnimation(float move)
    {
        if (move != 0)
        {
            anim.SetBool("isRunning", true);
            if (move > 0 && !facingRight)
            {
                CharacterFlip();
            }
            else if (move < 0 && facingRight)
            {
                CharacterFlip();
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void CharacterFlip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CharacterAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && move == 0)
        {
            isAttacking = true;
            anim.SetTrigger("isAttacking");
        }
    }

    void CharacterDashAttack()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) && move != 0))
        {
            anim.SetTrigger("isDashAttack");
        }
    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("isJumping", true);

            if (IsGrounded)
            {
                rb.velocity = Vector2.up * JumpForce;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("isJumping", false);

        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("isJumping", true);
        IsGrounded = false;
    }

    // Bu metodu Animator içinde saldýrý animasyonunun sonunda çaðýrýn
    public void EndAttack()
    {
        isAttacking = false;
    }
}