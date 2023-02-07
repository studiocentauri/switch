using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSwitch : MonoBehaviour
{
    public void InitiateSwitch() {
        if(this.transform.localScale.y < 0) {
            this.gameObject.GetComponent<Animator>().Play("Switch M to P");
        }
        else {
            this.gameObject.GetComponent<Animator>().Play("Switch P to M");
        }
        
        for(int i = 0; i < 2; i++) {
            Transform tmp = this.gameObject.transform.GetChild(i);
            for(int j = 0; j < tmp.gameObject.transform.childCount; j++) {
                tmp.gameObject.transform.GetChild(j).GetComponent<Obstacle>().PlayAnimation();
            }
        }
    }
}
