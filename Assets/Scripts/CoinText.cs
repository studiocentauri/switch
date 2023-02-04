using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void IncrementCoinCount (int coinTotal)
    {
        coinText.text = $"Coins: {coinTotal}";
    }
}
