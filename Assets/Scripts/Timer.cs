
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float CurrentTime;
  public TextMeshProUGUI TimerText;
    void Update(){
    CurrentTime +=Time.deltaTime;
    TimerText.text = CurrentTime.ToString();
}
}
