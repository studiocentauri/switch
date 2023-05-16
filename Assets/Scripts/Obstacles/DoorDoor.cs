using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDoor : MonoBehaviour
{
    public bool dooractive = true;
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (dooractive)
        {
            col.isTrigger = true;
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            col.isTrigger = false;
            col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        };
    }
}
