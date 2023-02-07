using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    float length, startpose;
    public GameObject cammale;
    public GameObject camfemale;
    private GameObject cam;
    public InputManager manage;
    public float parallax;

    private void Awake()
    {
        startpose = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        if (manage.currentPlayer == 0)
        {
            cam = cammale;
        }
        else cam = camfemale;


        float temp = cam.transform.position.x * (1 - parallax);
        float dist = cam.transform.position.x * parallax;

        transform.position = new Vector3( startpose + dist,transform.position.y, transform.position.z);
        
    }

    void LateUpdate()
    {
        if (manage.currentPlayer == 0)
        {
            cam = cammale;
        }
        else cam = camfemale;


        float temp = cam.transform.position.x * (1 - parallax);
        float dist = cam.transform.position.x * parallax;

        transform.position = new Vector3( Mathf.SmoothStep( transform.position.x, startpose + dist, 0.25f), transform.position.y, transform.position.z);

        if(temp > startpose + length) { startpose += length; }
        else if(temp < startpose - length) { startpose -= length; }
    }
}
