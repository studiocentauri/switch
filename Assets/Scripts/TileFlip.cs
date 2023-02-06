using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFlip : MonoBehaviour
{
    public void OnFlipTo()
    {
        //Time.timeScale = 0;
        Debug.Log("TimeScale 0");
    }

    public void OnFlipFrom()
    {
        //Time.timeScale = 1;
        Debug.Log("TimeScale 1");
    }
}
