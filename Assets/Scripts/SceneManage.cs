using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private static SceneManage instance;

    public static SceneManage GetInstance() {
        return instance;
    }
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayLevel(int level)
    {
        // Play Brush Stroke animation
        // Load the next scene only after completing the animation
        SceneManager.LoadSceneAsync(level);
    }
}
