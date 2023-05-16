using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics.Eventing.Reader;
// using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public ParticleSystem parSys;

    public GameObject dustCloud;

    public Rigidbody2D rb;

    public float acceleration = 5.0f;

    public float maxSpeed = 15.0f;

    public float friction = 0.1f;

    public bool isGroundDown = true;

    private List<Vector2> offset;

    public float detectRange = 0.1f;

    public bool isOnGround = false;

    public bool dummyIsOnGround = false;

    // private Vector2 gravityDir = new Vector2(0, 1);

    public float gravity = 15;

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
    private float halfWidth;
    private float halfHeight;

    Animator animator;
    private InputManager inputManager;

    public bool isActive=true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
        switchgravfac = isGroundDown ? 1 : -1;

        CapsuleCollider2D capsuleCollider2D = GetComponentInChildren<CapsuleCollider2D>();
        halfWidth = capsuleCollider2D.bounds.size.x / 2;
        halfHeight = capsuleCollider2D.bounds.size.y / 2;

        offset = new List<Vector2>();

        offset.Add(new Vector2(0, -halfHeight));
        offset.Add(new Vector2(halfWidth, -halfHeight));
        offset.Add(new Vector2(-halfWidth, -halfHeight));

        rb.gravityScale = isMale ? gravity : -gravity;
    }

    public void FixedUpdate()
    {
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

                    rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", false);
                    rb.gameObject.GetComponentInChildren<Animator>().SetBool("isSmash", false);

                    // apply camera shake if ground is touched after smash
                    if (isSmashing)
                    {
                        isSmashing = false;
                        CameraManagement.Instance.ShakeCamera(4f, .2f);
                        inputManager.ApplyGroundPound(hit.point.x);
                        Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
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
            Vector2 resVec = isGroundDown ? Vector2.up * jumpPow * jumpFactor : Vector2.down * jumpPow * jumpFactor;
            rb.AddForce(resVec, ForceMode2D.Impulse);
            dummyIsOnGround = false;
            isOnGround = false;

            rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", true);
            AudioManager.instance.PlaySound("Jump");
        }
    }

    public void ProcessPower()
    {
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
        rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", false);
        rb.gameObject.GetComponentInChildren<Animator>().SetBool("isSmash", true);
        rb.AddForce(isGroundDown ? Vector2.up * jumpPow : Vector2.down * jumpPow);
        canSpecial = false;
        rb.velocity = isGroundDown ? Vector2.down * smashvel : Vector2.up * smashvel;
        isSmashing = true;
    }

    private void Dash()
    {
        rb.gameObject.GetComponentInChildren<Animator>().SetBool("isJump", false);
        rb.gameObject.GetComponentInChildren<Animator>().SetBool("isSmash", true);
        parSys.gameObject.SetActive(true);
        
        Vector2 dashDirection = lookDirection * Vector2.right;
        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        canSpecial = false;
        AudioManager.instance.PlaySound("Dash");
        SyncParticles();
        Invoke("StopDash", dashDuration);
    }

    void StopDash()
    {
        parSys.gameObject.SetActive(false);
        Vector2 velocity = rb.velocity;
        velocity.x = 0.0f;
        rb.velocity = velocity;
    }

    public void Move(float horizontal)
    {
        
        animator.SetFloat("isRunning", Mathf.Abs(horizontal));
        Vector2 velocity = rb.velocity;
        if (canSpecial)
        {
            if (horizontal != 0.0f)
            {
                lookDirection = horizontal / Mathf.Abs(horizontal);
                velocity.x += acceleration * horizontal * Time.fixedDeltaTime;
                velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
                if (lookDirection > 0) GetComponentInChildren<SpriteRenderer>().flipX = false; else GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else
            {
                velocity.x = Mathf.Lerp(velocity.x, 0.0f, friction);

            }
        }
        rb.velocity = velocity;

    }

    void SyncParticles()
    {
        var main = parSys.main;
        main.startRotationZ = transform.rotation.eulerAngles.z * -Mathf.Deg2Rad;
    }
    
}
