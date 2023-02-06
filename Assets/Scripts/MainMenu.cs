using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLevel(int level)
    {
        // Play Brush Stroke animation
        // Load the next scene only after completing the animation
        SceneManager.LoadSceneAsync(level);
    }
}
