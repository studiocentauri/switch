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
    private bool isSmashing = false;

    public float smashvel = 30f;

    int switchgravfac;

    float lookDirection = 1;

    public float dashForce = 20.0f;

    public float dashDuration = 0.2f;

    Animator animator;
    private InputManager inputManager;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
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

                    if(isGroundDown && transform.position.y < 4.5f)
                    {
                        transform.position = new Vector2(transform.position.x, 4.5f);
                    }

                    rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", false);
                    rb.gameObject.GetComponentInChildren<Animator>().SetBool("isSmash", false);
                    // apply camera shake if ground is touched after smash
                    if (isSmashing)
                    {
                        isSmashing = false;
                        CameraManagement.Instance.ShakeCamera(4f, .2f);
                        inputManager.ApplyGroundPound(hit.point.x);
                    }

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

    public void ProcessJump(float jumpFactor)
    {
        if (dummyIsOnGround)
        {
            Debug.Log("Jumped");
            Vector2 resVec = isGroundDown ? Vector2.up * jumpPow * jumpFactor : Vector2.down * jumpPow * jumpFactor;
            rb.AddForce(resVec, ForceMode2D.Impulse);
            dummyIsOnGround = false;
            isOnGround = false;

            rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", true);
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
        rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", false);
        rb.gameObject.GetComponentInChildren<Animator>().SetBool("isSmash", true);
        rb.AddForce(isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow);
        canSpecial = false;
        rb.velocity = isGroundDown ? Vector2.down * smashvel : Vector2.up * smashvel;
        isSmashing = true;
    }

    private void Dash()
    {
        Debug.Log("Dash");
        Debug.Log("dir " + lookDirection);
        Vector2 dashDirection = lookDirection * Vector2.right;
        Debug.Log("Force is:- " + dashDirection * dashForce + " with Direction " + dashDirection);
        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        canSpecial = false;
        Invoke("StopDash", dashDuration);
    }

    void StopDash()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = 0.0f;
        rb.velocity = velocity;
    }

    public void Move(float horizontal)
    {
        Vector2 velocity = rb.velocity;
        if (canSpecial)
        {
            if (horizontal != 0.0f)
            {
                animator.SetBool("isRunning", true);
                lookDirection = horizontal / Mathf.Abs(horizontal);
                velocity.x += acceleration * horizontal * Time.fixedDeltaTime;
                velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
                if (lookDirection > 0) GetComponentInChildren<SpriteRenderer>().flipX = false; else GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator.SetBool("isRunning", false);
                velocity.x = Mathf.Lerp(velocity.x, 0.0f, friction);

            }
        }
        rb.velocity = velocity;
    }
}
