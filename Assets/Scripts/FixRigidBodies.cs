using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRigidBodies : MonoBehaviour
{
    void Awake() {
        for(int i = 0; i < this.transform.childCount; i++) {
            if(this.transform.GetChild(i).name == "Obstacle Set Straight") {
                Transform tmp = this.transform.GetChild(i);
                for(int j = 0; j < tmp.childCount; j++) {
                    Transform ttmp = tmp.transform.GetChild(j);
                    if(ttmp.GetComponent<Rigidbody2D>()) {
                        ttmp.GetComponent<Rigidbody2D>().gravityScale = 1;
                        ttmp.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                    }
                }
            }
            else if(this.transform.GetChild(i).name == "Obstacle Set Inverted") {
                Transform tmp = this.transform.GetChild(i);
                for(int j = 0; j < tmp.childCount; j++) {
                    Transform ttmp = tmp.transform.GetChild(j);
                    if(ttmp.GetComponent<Rigidbody2D>()) {
                        ttmp.GetComponent<Rigidbody2D>().gravityScale = -1;
                        ttmp.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                    }
                }
            }
        }
    }

    public void SwapGravity() {
        for(int i = 0; i < this.transform.childCount; i++) {
            if(this.transform.GetChild(i).name == "Obstacle Set Straight" || this.transform.GetChild(i).name == "Obstacle Set Inverted") {
                Transform tmp = this.transform.GetChild(i);
                for(int j = 0; j < tmp.childCount; j++) {
                    Transform ttmp = tmp.transform.GetChild(j);
                    if(ttmp.GetComponent<Rigidbody2D>()) {
                        ttmp.GetComponent<Rigidbody2D>().gravityScale *= -1;
                    }
                }
            }
        }
    } 
}
