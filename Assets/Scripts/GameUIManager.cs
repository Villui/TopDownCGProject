using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public static GameUIManager Instance;

    [SerializeField] private Image fadeImage;

    private float fadeTime = 10f;
    private float currentFadeTime = 0f;


    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;
    }

    public void EndGame() {
        StartCoroutine(FadeToBlackRoutine());
    }

    private IEnumerator FadeToBlackRoutine() {
        while (currentFadeTime < fadeTime) {
            currentFadeTime += Time.deltaTime;
            
            float newAlpha = currentFadeTime / fadeTime;
            newAlpha = Mathf.Clamp01(newAlpha);

            Color fadeImageColor = fadeImage.color;
            fadeImageColor.a = newAlpha;
            fadeImage.color = fadeImageColor;

            yield return null;
        }

        SceneManager.LoadScene(Loader.Scene.EndScene.ToString());
    }
}
