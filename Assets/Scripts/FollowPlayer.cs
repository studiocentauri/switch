using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;

    public Vector3 offset;

    private Controller P1;
    private Controller P2;

    private void Start()
    {
        P1 = GameObject.Find("P1").GetComponent<Controller>();
        P2 = GameObject.Find("P2").GetComponent<Controller>();
    }

    void Update()
    {
        target = P1.isActive ? P1.gameObject: P2.gameObject;
        transform.position = target.transform.position + offset;
        
    }
}
