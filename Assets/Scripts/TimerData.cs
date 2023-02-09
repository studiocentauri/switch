using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerData : MonoBehaviour
{
    [SerializeField] int optimalTime;
    [SerializeField] int timeLimit;
    public bool hasTimer = false;
    void Awake() {
        Score.instance.SetData(optimalTime, timeLimit, hasTimer);
    }
}
