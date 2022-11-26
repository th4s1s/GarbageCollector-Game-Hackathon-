using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinInStage : MonoBehaviour
{
    public Text coinText;
    void Update()
    {
        coinText.text = Player.Instance.coinInStage.ToString();
    }
}
