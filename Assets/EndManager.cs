using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    private void Start()
    {
        scoreText.text=Score.instance.GetScore().ToString();
    }
    public void NextLevel(int level)
    {
        SceneManage.instance.PlayLevel(level);
        Time.timeScale = 1f;
    }
    public void Replay()
    {
        SceneManage.instance.PlayLevel(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManage.instance.PlayLevel(0);
        Time.timeScale = 1f;
    }
}
