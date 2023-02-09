using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
   public int timeLimit=300;

   public int optimalTime=120;

   float timer=0.0f;

   bool countTime=true;

  public static Score instance;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

   void Update()
   {
        if(countTime)
        {
            timer+=Time.deltaTime;
        }
   }

   public int GetScore()
   {
    countTime=false;
    if(timer<optimalTime)
    {
        timer=optimalTime;
    }
    float score=100-(100*((timer-optimalTime)/(timeLimit-optimalTime)));
    return (int)score;
   }
}
