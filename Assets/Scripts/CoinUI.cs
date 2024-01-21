using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI coinText;

    private int startingCoinAmount = 0;


    private void Start() {
        PlayerCoinManager.Instance.OnCoinsAdded += HandleCoinsAdded;

        coinText.text = startingCoinAmount.ToString();
    }

    private void HandleCoinsAdded() {
        coinText.text = PlayerCoinManager.Instance.GetCurrentCoinAmount().ToString();
    }
}
