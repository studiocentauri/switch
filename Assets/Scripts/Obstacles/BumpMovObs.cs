using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpMovObs : MonoBehaviour
{
    public float dist = 5;
    public float ForceDist = 10;

    void Start()
    {
        InputManager.onSmash += InverseJump;
    }

    void Update()
    {
        
    }

    void InverseJump(float d)
    {
        if(Mathf.Abs(d - transform.position.x) < dist)
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * rb.gravityScale * ForceDist);
        }
    }
}
