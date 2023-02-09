using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapObs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Player Reset");
            SceneManage.instance.PlayLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
