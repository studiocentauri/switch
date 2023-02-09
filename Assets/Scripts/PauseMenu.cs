using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void OnExit() {
        Time.timeScale = 1f;
        SceneManage.instance.PlayLevel(0);
    }
}
