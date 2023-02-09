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
        sceneManage = SceneManage.instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log(other.tag);
        if (other.tag == "Player")
            sum++;
        if (sum == 2)
        {
           int score= GameObject.Find("ScoreManager").GetComponent<Score>().GetScore();
            sceneManage.PlayLevel(nexLevel);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            sum--;
    }
}
