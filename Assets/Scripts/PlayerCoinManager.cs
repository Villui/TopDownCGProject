using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinManager : MonoBehaviour {

    public static PlayerCoinManager Instance;

    public event Action OnCoinsAdded;

    private int currentCoinAmount;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player Coin Manager Instance already exists!");
            return;
        }

        Instance = this;
    }

    public void AddCoins(int coinAmount) {
        currentCoinAmount += coinAmount;
        OnCoinsAdded?.Invoke();
    }

    public int GetCurrentCoinAmount() {
        return currentCoinAmount;
    }
}
