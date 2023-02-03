using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rb;

    public float acceleration = 5.0f;

    public float maxSpeed = 15.0f;

    public float friction = 0.1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
