using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed;
    public bool isFacingRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.canMove)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(xInput * movementSpeed, 0);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (rb.linearVelocity != Vector2.zero)
        {
            GameManager.canPetShoot = false;
        }
        else
        {
            GameManager.canPetShoot = true;
        }

        Flip(rb.linearVelocity);
        
    }

    public void Flip(Vector2 direction)
    {
        if ((isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
