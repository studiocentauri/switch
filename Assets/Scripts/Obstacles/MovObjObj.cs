using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovObjObj : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0);

        Vector3 poi = Camera.main.WorldToScreenPoint(rb.position);
        if(poi.x < 0 || poi.x > Screen.width || poi.y < 0 || poi.y> Screen.height)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("Player Ded hahaha");
        }
    }
}
