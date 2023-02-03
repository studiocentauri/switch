using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb;

    public float acceleration = 5.0f;

    public float maxSpeed = 15.0f;

    public float friction = 0.1f;

    public bool isGroundDown = true;

    public Vector2[] offset;

    public float detectRange = 0.5f;

    private bool isOnGround = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isOnGround)
        {
            Debug.Log("Ground");
        }
        else
        {
            Debug.Log("Air");
        }
    }

    public void FixedUpdate()
    {
        foreach (Vector2 offseti in offset)
        {
            Debug.DrawRay(rb.position + offseti, Vector2.down * detectRange, Color.black, 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(rb.position + offseti, Vector2.down, detectRange, LayerMask.GetMask("Platform"));
            if (hit.collider != null)
            {
                Debug.Log("A: " + hit.collider.gameObject.tag + " B:" + hit.collider.gameObject.name);
                isOnGround = true;
                break;
            }
            else
            {
                isOnGround = false;
            }
        }
    }

    public void Move(float horizontal)
    {
        Vector2 velocity = rb.velocity;
        if (horizontal != 0.0f)
        {
            velocity.x += acceleration * horizontal * Time.fixedDeltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
        else
        {
            velocity.x = Mathf.Lerp(velocity.x, 0.0f, friction);
        }
        rb.velocity = velocity;
    }
}
