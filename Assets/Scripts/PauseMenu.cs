using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManage.instance.PlayLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExit() {
        Time.timeScale = 1f;
        SceneManage.instance.PlayLevel(0);
    }
}
