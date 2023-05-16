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
        Vector3 poi = Camera.main.WorldToScreenPoint(transform.position);
        if (poi.x > 0 && poi.x < Screen.width && poi.y > 0 && poi.y < Screen.height)
        {
            Destroy(gameObject);
        }

        currtime += Time.deltaTime;
        if (istrig && currtime >= spawntime)
        {
            currtime = 0;
            GameObject obj = Instantiate(movingObj, transform.position, transform.rotation);
            obj.transform.parent = null;
            if(obj.transform.position.y < - 2.5)
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, -1 * obj.transform.localScale.y,obj.transform.localScale.z);
                obj.GetComponent<Rigidbody2D>().gravityScale = -15;
            }
            else
            {
                obj.GetComponent<Rigidbody2D>().gravityScale = 15;
            }


        }
    }
}
