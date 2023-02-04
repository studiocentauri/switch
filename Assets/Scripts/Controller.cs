using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D rb;

    public float acceleration = 5.0f;

    public float maxSpeed = 15.0f;

    public float friction = 0.1f;

    public bool isGroundDown = true;

    public List<Vector2> offset;

    public float detectRange = 0.1f;

    public bool isOnGround = false;

    public bool dummyIsOnGround = false;

    private Vector2 gravityDir = new Vector2(0, 1);

    public float gravity = 50;

    public float jumpPow = 120;

    public float jumpWaitTimer = 0.2f;

    public float specialWaitTimer = 0.2f;

    public float smashDelatTimer = 0.2f;

    private bool canSpecial = false;

    public bool isMale = true;

    public float smashvel = 30f;

    int switchgravfac;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        switchgravfac = isGroundDown ? 1 : -1;
    }

    public void FixedUpdate()
    {
        DoGravity();
        if (rb.velocity.y * switchgravfac <= 0.01f)
        {
            foreach (Vector2 offseti in offset)
            {
                Debug.DrawRay(rb.position + Vector2.Scale(offseti, new Vector2(1, switchgravfac)), Vector2.down * detectRange * switchgravfac, Color.black, 0.1f);
                RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.Scale(offseti, new Vector2(1, switchgravfac)), Vector2.down * switchgravfac, detectRange, LayerMask.GetMask("Platform"));
                if (hit.collider != null)
                {
                    isOnGround = true;
                    dummyIsOnGround = true;
                    canSpecial = true;
                    break;
                }
                else
                {
                    if (isOnGround)
                    {
                        Invoke("WaitForJump", jumpWaitTimer);
                        isOnGround = false;
                    }
                }
            }
        }
    }

    private void WaitForJump()
    {
        dummyIsOnGround = false;
    }

    public void ProcessJump()
    {
        if (dummyIsOnGround)
        {
            Debug.Log("Jumped");
            Vector2 resVec = isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow;
            rb.AddForce(resVec, ForceMode2D.Impulse);
            dummyIsOnGround = false;
            isOnGround = false;
        }
    }

    public void DoGravity()
    {
        if (isGroundDown)
        {
            if (!isOnGround)
            {
                rb.AddForce(gravity * gravityDir * -1);
            }
        }
        else
        {
            if (!isOnGround)
            {
                rb.AddForce(gravityDir * gravity);
            }
        }
    }

    public void ProcessPower()
    {
        Debug.Log("Power Use");
        if (isMale && !isOnGround && canSpecial)
        {
            Smash();
        }
        else if (!isMale && !isOnGround && canSpecial)
        {
            Dash();
        }
    }

    private void Smash()
    {
        Debug.Log("Smash");
        rb.AddForce(isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow);
        canSpecial = false;
        rb.velocity = isGroundDown ? Vector2.down * smashvel : Vector2.up * smashvel;
    }

    private void Dash()
    {

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
