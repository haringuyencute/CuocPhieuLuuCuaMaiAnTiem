using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 1f;           // T?c ?? di chuy?n
    public float moveDistance = 3f;       // Kho?ng cách di chuy?n t?i ?a t? ?i?m ban ??u
    public Transform player;              // Tham chi?u ??n nhân v?t
    public float detectionRange = 10f;    // Kho?ng cách ?? kích ho?t di chuy?n
    private bool movingRight = true;      // ?i?u khi?n h??ng di chuy?n
    private float startPositionX;         // L?u v? trí ban ??u c?a quái v?t
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPositionX = transform.position.x;  // Ghi l?i v? trí ban ??u c?a quái v?t
    }

    void Update()
    {
        // Ki?m tra xem nhân v?t có trong ph?m vi hay không
        if (IsPlayerInRange())
        {
            MoveMonster();
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    bool IsPlayerInRange()
    {
        // Ki?m tra kho?ng cách gi?a quái v?t và nhân v?t
        if (player == null)
        {
            Debug.LogWarning("Player is not assigned to the MonsterMovement script.");
            return false;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    void MoveMonster()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Di chuy?n ph?i
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Di chuy?n trái
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi va ch?m v?i b?t k? v?t th? nào theo chi?u x
        if (collision.contacts[0].normal.x != 0) // Ki?m tra va ch?m theo chi?u ngang
        {
            movingRight = !movingRight; // ??i h??ng
        }
    }
}
