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
        transform.parent = null;
        Vector3 poi = Camera.main.WorldToScreenPoint(transform.position);
        if (poi.x > 0 && poi.x < Screen.width && poi.y > 0 && poi.y < Screen.height)
        {
            Destroy(gameObject);
        }

        currtime += Time.deltaTime;
        if (istrig && currtime >= spawntime)
        {
            currtime = 0;
            GameObject obj = Instantiate(movingObj, transform);
            obj.transform.parent = null;
        }
    }
}
