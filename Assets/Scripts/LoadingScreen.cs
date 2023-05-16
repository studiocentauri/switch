using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Image progressBar;

    void Start()
    {
        Debug.Log("Inside Loading Scene");
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation loadinglevel = SceneManager.LoadSceneAsync(SceneManage.instance.GetCntLevel());
        while (loadinglevel.progress < 1)
        {
            progressBar.fillAmount = loadinglevel.progress;
            yield return null;
        }
    }
}