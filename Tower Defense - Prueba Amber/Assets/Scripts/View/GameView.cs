using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainingZombiesText;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private CanvasGroup endGameScreen;

    private void Start()
    {
        // Subscribe to EventController Events
        EventsController.current.OnUpdateRemainingZombies += UpdateRemainingZombiesText;
        EventsController.current.OnWinGame += ShowWinScreen;
        EventsController.current.OnLoseGame += ShowLoseScreen;

        endGameScreen.blocksRaycasts = false;
    }

    private void OnDestroy()
    {
        // Unsubscribe to EventController Events
        EventsController.current.OnUpdateRemainingZombies -= UpdateRemainingZombiesText;
        EventsController.current.OnWinGame -= ShowWinScreen;
        EventsController.current.OnLoseGame -= ShowLoseScreen;
    }

    public void ResetGame()
    {
        StartCoroutine(FadeOut());
    }

    private void UpdateRemainingZombiesText(int remainingZombies)
    {
        remainingZombiesText.text = "Remaining Zombies: " + remainingZombies;
    }

    private void ShowWinScreen()
    {
        StartCoroutine(FadeInWinScreen());
    }
    
    private void ShowLoseScreen()
    {
        StartCoroutine(FadeInLoseScreen());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        
        endGameScreen.blocksRaycasts = false;
        EventsController.current.ResetGame();
        
        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.LerpUnclamped(1f, 0f, elapsedTime / 2f);
            endGameScreen.alpha = currentAlpha * 1;
            yield return null;
        }
    }
    
    private IEnumerator FadeInWinScreen()
    {
        float elapsedTime = 0f;
        
        endGameScreen.blocksRaycasts = true;

        winText.SetActive(true);
        loseText.SetActive(false);

        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.LerpUnclamped(0f, 1f, elapsedTime / 2f);
            endGameScreen.alpha = currentAlpha * 1;
            yield return null;
        }
    }
    
    private IEnumerator FadeInLoseScreen()
    {
        float elapsedTime = 0f;
        endGameScreen.blocksRaycasts = true;

        winText.SetActive(false);
        loseText.SetActive(true);

        while (elapsedTime < 2f)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.LerpUnclamped(0f, 1f, elapsedTime / 2f);
            endGameScreen.alpha = currentAlpha * 1;
            yield return null;
        }
    }

}
