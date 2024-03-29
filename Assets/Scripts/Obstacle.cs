using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void PlayAnimation() {
        this.gameObject.GetComponent<Animator>().Play("SwitchFade");
    }
    public void DisableCollider() {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void EnableCollider() {
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
