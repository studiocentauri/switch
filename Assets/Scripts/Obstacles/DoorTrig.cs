using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrig : MonoBehaviour
{
    public bool isontrig = true;
    public DoorDoor door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");
        if(collision.tag == "Player")
        {
            if (isontrig)
            {
                door.dooractive = true;
            }
            else
            {
                door.dooractive = false;
            }
        }
    }
}
