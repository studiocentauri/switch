using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObsPartHelperMono : MonoBehaviour
{
    public GameObject Straight;
    public GameObject Inverted;
    public GameObject Parent;
    public float trs = -2.5f;

    public void Organize()
    {
        int childcount = Parent.transform.childCount;

        for(int i = 0; i<childcount; i++)
        {
            Transform chi = Parent.transform.GetChild(i);
            if(chi.position.y > trs)
            {
                chi.parent = Straight.transform;
            }
            else
            {
                chi.parent = Inverted.transform;
            }
        }

    }
}
