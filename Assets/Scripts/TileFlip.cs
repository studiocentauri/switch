using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFlip : MonoBehaviour
{
    Rigidbody2D P1;
    Rigidbody2D P2;

    private void Start()
    {
        P1 = GameObject.Find("P1").GetComponent<Rigidbody2D>();
        P2 = GameObject.Find("P2").GetComponent<Rigidbody2D>();
    }
    public void OnFlipTo()
    {
        P1.constraints = RigidbodyConstraints2D.FreezeAll;        
        P1.gameObject.GetComponentInChildren<Animator>().speed = 0;
        P2.constraints = RigidbodyConstraints2D.FreezeAll;        
        P2.gameObject.GetComponentInChildren<Animator>().speed = 0;
        //Time.timeScale = 0;
    }

    public void OnFlipFrom()
    {
        P1.constraints = RigidbodyConstraints2D.FreezeRotation;
        P1.gameObject.GetComponentInChildren<Animator>().speed = 1;
        P2.constraints = RigidbodyConstraints2D.FreezeRotation;
        P2.gameObject.GetComponentInChildren<Animator>().speed = 1;
        //Time.timeScale = 1;
    }
}
