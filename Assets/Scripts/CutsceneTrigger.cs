using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    int sum = 0;
    SceneManage sceneManage;
    [SerializeField] int nexLevel;
    private void Awake()
    {
        sceneManage = SceneManage.GetInstance();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
            sum++;
        if (sum == 2)
            sceneManage.PlayLevel(nexLevel);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
            sum--;
    }
}
