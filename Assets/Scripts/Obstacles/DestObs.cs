using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestObs : MonoBehaviour
{
    public float smashDist = 5;
    void Start()
    {
        InputManager.onSmash += Smashed;
        
    }

    void Smashed(float gx)
    {
        if(Mathf.Abs(gx - transform.position.x) < smashDist)
        {
            gameObject.GetComponent<Explodable>().explode();
            InputManager.onSmash-= Smashed;
        }
    }
}
