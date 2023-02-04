using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //  Coins must contain the following script
    //   public static event Action OnCoinCollected;
    [SerializeField] CoinText coinText;
    [SerializeField] TextMeshProUGUI highScoreText;
    int coinCount;
  
    private void OnEnable(){
        Coin.OnCoinCollected += HandleCoinPickup;
    }
    private void OnDisable(){
        Coin.OnCoinCollected -= HandleCoinPickup;
    }
    private void Start(){
        UpdateHighScoreText();
    }
    void HandleCoinPickup(){
        coinCount++;
        coinText.IncrementCoinCount(coinCount);
     PlayerPrefs.SetInt("HighScore",coinCount);
     PlayerPrefs.GetInt("HighScore");
    }
    void CheckHighScore(){
        if (coinCount> PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore",coinCount);
          //Dynamically Updaing High Score
          UpdateHighScoreText();
        }
    }
    void UpdateHighScoreText(){ 
        highScoreText.text = $" HighScore :{ PlayerPrefs.GetInt("Highscore",0)}";
    }
}
//     int HighScore = 0;
// void Update()
// {
//     HandleCoinPickup();
//     print(PlayerPrefs.GetInt("Highscore", HighScore));
// }
// void HandleCoinPickup()
// {
//     HighScore = PlayerPrefs.GetInt("HighScore", HighScore); 

//     if (points>HighScore)
//     {
//         HighScore = points;
//         PlayerPrefs.SetInt("HighScore", HighScore);
//         PlayerPrefs.Save();
//     }
// }