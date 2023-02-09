using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;
    public static int cnt_level = 0;
    private List<int> playable_levels = new List<int>{6, 7, 9};
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
        cnt_level = level;
        SceneManager.LoadSceneAsync(1);
        if(GameManager.instance) {
            if(playable_levels.Contains(level)) {
                GameManager.instance.GetComponent<InputManager>().enabled = true;
            }
            else {
                GameManager.instance.GetComponent<InputManager>().enabled = false;
            }
        }
    }

    public int GetSceneID()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int GetCntLevel() {
        return cnt_level;
    }
}
