using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;
    private bool isFacingRight = true;


    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the player object.");
        }
    }

    void Update()
    {
        playerMovement();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void playerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }

    }
    void MovePlayer()
    {
        movement.Normalize(); // Ensure consistent speed in all directions
        rb.velocity = movement * speed;
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
    
}
