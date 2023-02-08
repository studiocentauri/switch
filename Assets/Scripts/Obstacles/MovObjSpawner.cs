using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovObjSpawner : MonoBehaviour
{
    public GameObject movingObj;
    public bool istrig = false;
    public float spawntime = 3f;
    private float currtime = 0;

    void Update()
    {
        currtime += Time.deltaTime;
        if(istrig && currtime >= spawntime)
        {
            currtime = 0;
            Instantiate(movingObj, transform);
        }
    }
}
