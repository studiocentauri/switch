using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    int sum = 0;
    SceneManage sceneManage;
    [SerializeField] int nexLevel;
    [SerializeField] GameObject endPrefab;
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
            // sceneManage.PlayLevel(nexLevel);
            endPrefab.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            sum--;
    }
}
