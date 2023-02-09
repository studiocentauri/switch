using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovObjTrig : MonoBehaviour
{
    public MovObjSpawner spanwer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spanwer.istrig = true;
        }
    }
}
