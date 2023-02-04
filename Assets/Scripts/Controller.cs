using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody2D rb;

    public float acceleration = 5.0f;

    public float maxSpeed = 15.0f;

    public float friction = 0.1f;

    public bool isGroundDown = true;

    public Vector2[] offset;

    public float detectRange = 0.5f;

    private bool isOnGround = true;

    private Vector2 gravityDir = new Vector2(0, 1);

    public float gravity = 10;

    public float jumpPow = 60;

    public float jumpWaitTimer = 5f;

    private bool wasOnGround = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void FixedUpdate()
    {

        DoGravity();


        foreach (Vector2 offseti in offset)
        {
            int switchgravfac = isGroundDown ? 1 : -1;
            Debug.DrawRay(rb.position + Vector2.Scale(offseti, new Vector2(1, switchgravfac)), Vector2.down * detectRange*switchgravfac, Color.black, 0.1f);
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.Scale(offseti, new Vector2(1, switchgravfac)), Vector2.down*switchgravfac, detectRange, LayerMask.GetMask("Platform"));
            if (hit.collider != null)
            {
                isOnGround = true;
                wasOnGround = true;
                break;
            }
            else
            {
                if (wasOnGround)
                {
                    StartCoroutine(waitForJump(jumpWaitTimer));
                    Debug.Log("Started Coroutine");
                    wasOnGround = false;
                }
            }
        }
    }

    public void processJump()
    {
        if (Input.GetAxis("Jump") > 0 && isOnGround)
        {
            rb.AddForce(isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow);
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

    private IEnumerator waitForJump(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isOnGround = false;
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
