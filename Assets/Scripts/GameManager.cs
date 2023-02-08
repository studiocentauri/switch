using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake() {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void PlayLevel(int level)
    {
        // Play Brush Stroke animation
        // Load the next scene only after completing the animation
        SceneManager.LoadSceneAsync(level);
        this.gameObject.GetComponent<InputManager>().enabled = true;
    }

    public int GetSceneID()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
