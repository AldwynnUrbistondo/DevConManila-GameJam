using UnityEngine;

public class PlayerMoveMain : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private float moveTimer = 0f;
    private int direction = 1; // 1 = right, -1 = left
    public float switchTime = 2f; // seconds before switching

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move the player
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Update timer
        moveTimer += Time.deltaTime;

        // Switch direction every 2 seconds
        if (moveTimer >= switchTime)
        {
            direction *= -1; // flip direction
            moveTimer = 0f;   // reset timer
        }
    }
}
