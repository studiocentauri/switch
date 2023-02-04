using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
        

        if(Input.GetKeyDown("space"))
        {
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        }

       
    }
}
