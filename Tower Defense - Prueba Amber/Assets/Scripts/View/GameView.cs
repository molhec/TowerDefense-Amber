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
        EventsController.current.OnUpdateRemainingZombies += UpdateRemainingZombiesText;
        EventsController.current.OnWinGame += ShowWinScreen;
        EventsController.current.OnLoseGame += ShowLoseScreen;
    }

    private void OnDestroy()
    {
        EventsController.current.OnUpdateRemainingZombies -= UpdateRemainingZombiesText;
        EventsController.current.OnWinGame -= ShowWinScreen;
        EventsController.current.OnLoseGame -= ShowLoseScreen;
    }

    public void ResetGame()
    {
        endGameScreen.alpha = 0f;
        EventsController.current.ResetGame();
    }

    private void UpdateRemainingZombiesText(int remainingZombies)
    {
        remainingZombiesText.text = "Remaining Zombies: " + remainingZombies;
    }

    private void ShowWinScreen()
    {
        winText.SetActive(true);
        loseText.SetActive(false);
        endGameScreen.alpha = 1f;
    }
    
    private void ShowLoseScreen()
    {
        winText.SetActive(false);
        loseText.SetActive(true);
        endGameScreen.alpha = 1f;
    }
}
