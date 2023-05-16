using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlackBg : MonoBehaviour
{
    public GameObject blackBg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            blackBg.SetActive(false);
            GameObject.Find("GameManager").GetComponent<InputManager>().canSwitch = true;
        }
    }
}
