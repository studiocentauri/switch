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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {

        DoGravity();
        
        int switchgravfac = isGroundDown ? 1 : -1;

        if (rb.velocity.y * switchgravfac < 0)
        {


            foreach (Vector2 offseti in offset)
            {

                Debug.DrawRay(rb.position + Vector2.Scale(offseti, new Vector2(1, switchgravfac)), Vector2.down * detectRange * switchgravfac, Color.black, 0.1f);
                RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.Scale(offseti, new Vector2(1, switchgravfac)), Vector2.down * switchgravfac, detectRange, LayerMask.GetMask("Platform"));

                if (hit.collider != null)
                {
                    if (!canSpecial && !isOnGround)
                    {
                        Invoke("restartSpecial", specialWaitTimer);
                    }
                    isOnGround = true;
                    dummyIsOnGround = true;
                    break;
                }
                else
                {
                    if (isOnGround)
                    {
                        Invoke("waitForJump", jumpWaitTimer);
                        isOnGround = false;
                    }
                }
            }
        }
    }

    private void restartSpecial()
    {
        canSpecial = true;
    }

    public void processJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dummyIsOnGround)
        {
            Debug.Log("Jumped");
            Vector2 resVec = isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow;
            rb.AddForce(resVec);
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

    private void waitForJump()
    {
        dummyIsOnGround = false;
    }

    public void processPower()
    {
        if(isMale && !isOnGround && canSpecial && Input.GetKeyDown(KeyCode.Space) )
        {
            smash();
        }
        else if(!isMale && !isOnGround && canSpecial && Input.GetKeyDown(KeyCode.Space))
        {
            dash();
        }

    }

    private void smash()
    {
        //rb.AddForce(isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow);
        canSpecial = false;
        rb.velocity = isGroundDown ? Vector2.down * smashvel : Vector2.up * smashvel;
    }

    private void dash()
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
