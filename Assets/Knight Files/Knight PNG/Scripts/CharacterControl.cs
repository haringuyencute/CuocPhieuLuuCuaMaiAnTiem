using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;      // Tốc độ di chuyển
    [SerializeField] private float jumpForce = 6f;     // Lực nhảy
    [SerializeField] private LayerMask groundLayer;     // Mặt đất
    GameManager gameManager;
    private bool isGrounded;                            // Kiểm tra nhân vật có đang đứng trên mặt đất hay không
    private Rigidbody2D rb;                             // Rigidbody2D của nhân vật
    private Animator anim;                              // Animator của nhân vật
    private bool facingRight = true;                    // Kiểm tra hướng nhân vật đang đối diện
    private PhysicsMaterial2D noFriction;
    // Các điểm kiểm tra mặt đất
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        // Lấy Rigidbody2D của nhân vật
        anim = GetComponent<Animator>();         // Lấy Animator của nhân vật
        noFriction = new PhysicsMaterial2D();
        noFriction.friction = 0f; // Thiết lập ma sát = 0
        noFriction.bounciness = 0f; // Đặt độ nẩy nếu cần

        // Gán vật liệu này cho collider của nhân vật
        GetComponent<Collider2D>().sharedMaterial = noFriction;
    }
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        // Kiểm tra nếu nhân vật đang đứng trên mặt đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Lấy input di chuyển từ người chơi (phím A/D hoặc mũi tên trái/phải)
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Xử lý hướng di chuyển
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Ưu tiên phát animation nhảy nếu nhân vật không chạm đất
        if (!isGrounded)
        {
            anim.Play("JumpCharacter");
        }
        else if (Mathf.Abs(moveInput) > 0)
        {
            anim.Play("WalkCharacter");
        }
        else
        {
            anim.Play("IdleCharacter");
        }

        // Xử lý nhảy
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Quay nhân vật theo hướng di chuyển
    private void Flip()
    {
        facingRight = !facingRight;

        // Lật ngược chiều x của nhân vật
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Vector2 contactNormal = collision.contacts[0].normal;

            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject); // Quái vật biến mất
                anim.Play("JumpCharacter");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (Mathf.Abs(contactNormal.x) > 0.5f) // Va chạm bên trái/phải
            {
                anim.Play("HurtCharacter");
                gameManager.TakeDamage();

                // Tạm thời vô hiệu hoá va chạm với quái vật
                Collider2D monsterCollider = collision.collider;
                Debug.LogError("loi!");
            }
        }
    }
}
