using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerData : MonoBehaviour
{
    [SerializeField] int optimalTime;
    [SerializeField] int timeLimit;
    void Start() {
        Score.instance.SetData(optimalTime, timeLimit);
    }
}
